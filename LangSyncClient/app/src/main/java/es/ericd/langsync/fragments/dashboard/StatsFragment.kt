package es.ericd.langsync.fragments.dashboard

import android.os.Bundle
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.lifecycle.lifecycleScope
import androidx.recyclerview.widget.LinearLayoutManager
import com.google.android.material.snackbar.Snackbar
import es.ericd.langsync.R
import es.ericd.langsync.adapters.RankingPointsAdapter
import es.ericd.langsync.databinding.FragmentStatsBinding
import es.ericd.langsync.dataclases.PlayerPoints
import es.ericd.langsync.services.FirebaseAuth
import es.ericd.langsync.services.FirestoreService
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch
import kotlinx.coroutines.withContext


class StatsFragment : Fragment() {
    // TODO: Rename and change types of parameters
    private var param1: String? = null
    private var param2: String? = null

    private var playersList = mutableListOf<PlayerPoints>()

    lateinit var binding: FragmentStatsBinding

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
    }

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {

        binding = FragmentStatsBinding.inflate(inflater)

        binding.recView.layoutManager = LinearLayoutManager(requireContext(), LinearLayoutManager.VERTICAL, false)
        binding.recView.adapter = RankingPointsAdapter(requireContext(), playersList)

        getStats()


        return binding.root
    }

    fun getStats() {

        lifecycleScope.launch(Dispatchers.IO) {

            var stats = FirestoreService.getStats()

            withContext(Dispatchers.Main) {

                if (stats.partyData.isEmpty()) return@withContext

                val totalPostion = stats.partyData.sumOf { it.position }

                binding.tvAvgPosition.text = (totalPostion.toDouble() / stats.partyData.size).toString()

                /* List of verbs */

                val listOfMostFailedVerbs = mutableMapOf<String, Int>()

                stats.partyData.forEach {
                    it.data.forEach {
                        if (!it.isCorrect) {

                            if (listOfMostFailedVerbs[it.toGet] != null) {
                                listOfMostFailedVerbs[it.toGet] = listOfMostFailedVerbs[it.toGet]!! + 1
                            } else {
                                listOfMostFailedVerbs[it.toGet] = 0
                            }
                        }
                    }
                }

                val mostFailed = listOfMostFailedVerbs.maxByOrNull { it.value }

                binding.tvMostFailedVerb.text = mostFailed?.key ?: "Not found"

            }
        }

        lifecycleScope.launch(Dispatchers.IO) {
            val stats = FirestoreService.getAllPlayersPoints()

            withContext(Dispatchers.Main) {

                val position = stats.indexOfFirst {
                    it.player.equals(FirebaseAuth.getCurrentUser()?.displayName ?: "")
                }

                if (position == -1) {
                    binding.tvRanking.text = "Not found"
                } else {
                    binding.tvRanking.text = (position + 1).toString()
                }

                playersList.clear()
                playersList.addAll(stats)

                binding.recView.adapter?.notifyDataSetChanged()


            }


        }

    }

}