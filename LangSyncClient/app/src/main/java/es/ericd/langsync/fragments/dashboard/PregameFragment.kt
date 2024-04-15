package es.ericd.langsync.fragments.dashboard

import android.os.Bundle
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.TextView
import android.widget.Toast
import androidx.lifecycle.lifecycleScope
import es.ericd.langsync.R
import es.ericd.langsync.databinding.FragmentPregameBinding
import es.ericd.langsync.services.FirestoreService
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch
import kotlinx.coroutines.withContext

class PregameFragment() : Fragment() {

    lateinit var binding: FragmentPregameBinding
    var partyCode: String = ""

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)

        arguments.let {
            partyCode = it?.getString(PARTY_CODE, "").toString()
        }

    }

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {

        binding = FragmentPregameBinding.inflate(inflater)

        binding.button3.setOnClickListener {
            leaveGame()
        }

        setListener()

        return binding.root
    }

    fun setListener() {
        lifecycleScope.launch(Dispatchers.IO) {
            FirestoreService.getDocPlayersChanges(partyCode).collect {
                withContext(Dispatchers.Main) {

                    if (it != null) {

                        binding.scrollViewLinearLayout.removeAllViews()

                        it.forEach {player ->
                            val playerLabel = TextView(requireContext())

                            playerLabel.text = player

                            binding.scrollViewLinearLayout.addView(playerLabel)

                        }


                    }

                }
            }
        }
    }

    fun leaveGame() {
        lifecycleScope.launch (Dispatchers.IO) {
            FirestoreService.leaveGame(partyCode)

            withContext(Dispatchers.Main) {
                // go to prev fragment
                requireActivity().onBackPressedDispatcher.onBackPressed()
            }

        }
    }

    companion object {
        val PARTY_CODE = "PARTY_CODE"
    }

}