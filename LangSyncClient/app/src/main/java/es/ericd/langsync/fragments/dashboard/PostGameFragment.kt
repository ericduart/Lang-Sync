package es.ericd.langsync.fragments.dashboard

import android.content.Context
import android.os.Bundle
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.lifecycle.lifecycleScope
import androidx.recyclerview.widget.LinearLayoutManager
import es.ericd.langsync.R
import es.ericd.langsync.adapters.RankingAdapter
import es.ericd.langsync.databinding.FragmentPostGameBinding
import es.ericd.langsync.dataclases.FirestorePlayersChosesDataClass
import es.ericd.langsync.dataclases.FirestorePlayersDataGrammar
import es.ericd.langsync.fragments.dialogs.PlayPrivateGameDialogFragment
import es.ericd.langsync.fragments.dialogs.ShowDataPostGameDialogFragment
import es.ericd.langsync.services.FirebaseAuth
import es.ericd.langsync.services.FirestoreService
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch
import kotlinx.coroutines.withContext

class PostGameFragment : Fragment() {

    lateinit var binding: FragmentPostGameBinding
    lateinit var partyCode: String
    var players = mutableListOf<FirestorePlayersChosesDataClass>()
    var mInterface: PostGameInterface? = null

    override fun onAttach(context: Context) {
        super.onAttach(context)

        if (context is PostGameInterface) {
            mInterface = context
        } else throw Exception("Activity must implement PostGameInterface")

    }

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)

        arguments?.let {
            partyCode = it.getString(PARTY_CODE, "")
        }

    }

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {

        binding = FragmentPostGameBinding.inflate(inflater)

        val inputClick = { inputs: List<FirestorePlayersDataGrammar> ->

            ShowDataPostGameDialogFragment("", inputs.toMutableList()).show(requireActivity().supportFragmentManager, "")
        }

        binding.rankingRecView.adapter = RankingAdapter(requireContext(), players, inputClick)
        binding.rankingRecView.layoutManager = LinearLayoutManager(requireContext(), LinearLayoutManager.VERTICAL, false)


        // setRecview()

        init()

        binding.btnCurrentUserData.setOnClickListener {
            val currentPlayer = players.find { it.user.equals(FirebaseAuth.getCurrentUser()?.displayName) }
            if (players.isEmpty()) return@setOnClickListener
            if (currentPlayer == null) return@setOnClickListener

            ShowDataPostGameDialogFragment(currentPlayer.user, currentPlayer.grammar.toMutableList()).show(requireActivity().supportFragmentManager, "")

        }

        binding.btnLeave.setOnClickListener {
            mInterface?.goBackToMenu()
        }

        return binding.root
    }

    fun saveStats(plist: MutableList<FirestorePlayersChosesDataClass>) {

        val player = plist.find { it.user.equals(FirebaseAuth.getCurrentUser()?.displayName) }

        if (player == null) return

        val position = plist.indexOfFirst { it.user.equals(FirebaseAuth.getCurrentUser()?.displayName) } + 1

        lifecycleScope.launch(Dispatchers.IO) {
            FirestoreService.saveStats(partyCode, player, position)
        }

    }

    fun init() {
        lifecycleScope.launch(Dispatchers.IO) {
            val data = FirestoreService.getPlayersDataAfterGame(partyCode)

            withContext(Dispatchers.Main) {
                setRecview(data)
                saveStats(data)
            }
        }
    }

    fun setRecview(plist: MutableList<FirestorePlayersChosesDataClass>) {
        players.clear()
        plist.forEach {
            players.add(it)
        }

        binding.rankingRecView.adapter?.notifyDataSetChanged()
    }

    interface PostGameInterface {
        fun goBackToMenu()
    }

    companion object {
        val PARTY_CODE = "PARTY_CODE"
    }
}