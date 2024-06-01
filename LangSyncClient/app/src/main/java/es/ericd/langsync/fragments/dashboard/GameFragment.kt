package es.ericd.langsync.fragments.dashboard

import android.content.Context
import android.os.Bundle
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.lifecycle.lifecycleScope
import es.ericd.langsync.R
import es.ericd.langsync.databinding.FragmentGameBinding
import es.ericd.langsync.services.FirebaseAuth
import es.ericd.langsync.services.FirestoreService
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.Job
import kotlinx.coroutines.launch
import kotlinx.coroutines.withContext

class GameFragment : Fragment() {

    private var partyCode: String = ""
    private var grammarEnglish: String = ""
    private var grammarSpanish: String = ""
    private var grammarList: List<HashMap<String, String>> = emptyList()
    lateinit var binding: FragmentGameBinding

    private var listenerJob: Job? = null


    var mInterace: GameFragmentInterface? = null

    override fun onAttach(context: Context) {
        super.onAttach(context)

        if (context is GameFragmentInterface) {
            mInterace = context
        } else {
            throw Exception("Activity must implement GameFragmenInterface")
        }

    }

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        arguments?.let {
            partyCode = it.getString(PARTY_CODE).toString()
            grammarEnglish = it.getString(GRAMMAR_ENGLISH).toString()
            grammarSpanish = it.getString(GRAMMAR_SPANISH).toString()

        }
    }

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {

        binding = FragmentGameBinding.inflate(inflater)

        binding.tvCurrentGrammar.text = grammarEnglish
        binding.tvPoints.text = "0"
        binding.tvPosition.text = "1"

        setListeners()
        setPlayersListener()

        binding.btnSend.setOnClickListener {
            lifecycleScope.launch(Dispatchers.IO) {
                FirestoreService.setPlayerGrammar(partyCode, grammarList, binding.etPlayerGrammar.text.toString(), grammarEnglish)

                withContext(Dispatchers.Main) {
                    binding.etPlayerGrammar.text.clear()
                }
            }
        }

        return binding.root
    }

    fun setListeners() {
        listenerJob = lifecycleScope.launch(Dispatchers.IO) {
            FirestoreService.getDocPlayersChanges(partyCode, GAME).collect {
                withContext(Dispatchers.Main) {

                    val prevEnglish = grammarEnglish

                    grammarEnglish = it.grammar.get("english").toString()
                    grammarSpanish = it.grammar.get("spanish").toString()

                    if (prevEnglish != grammarEnglish) {
                        binding.tvCurrentGrammar.text = grammarEnglish
                    }

                    grammarList = it.grammarList

                    if (it.gameEnded) {
                        stopListener()
                        mInterace?.showEndingGameFragment(partyCode)
                    }

                }
            }

        }
    }

    fun stopListener() {
        if (listenerJob?.isActive ?: false) {
            listenerJob?.cancel()
        }

    }

    fun setPlayersListener() {
        lifecycleScope.launch(Dispatchers.IO) {
            FirestoreService.getCollectionPlayersDataChanges(partyCode).collect {
                val sortedData = it.sortedByDescending {
                    it.grammar.count { it.isCorrect }
                }

                withContext(Dispatchers.Main) {
                    val position = sortedData.indexOfFirst { it.user.equals(FirebaseAuth.getCurrentUser()?.displayName) }

                    if (position != -1) {
                        binding.tvPosition.text = "${position + 1}"
                    } else {
                        binding.tvPosition.text = "${sortedData.size}"
                    }

                    val currentPlayerData = sortedData.find { it.user.equals(FirebaseAuth.getCurrentUser()?.displayName) }

                    val numPoints = currentPlayerData?.grammar?.count{it.isCorrect}

                    binding.tvPoints.text = "${numPoints ?: "0"}"


                }
            }

        }
    }

    interface GameFragmentInterface {
        fun showEndingGameFragment(partyCode: String)
    }

    companion object {
        val PARTY_CODE = "PARTY_CODE"
        val GRAMMAR_ENGLISH = "GRAMMAR_ENGLISH"
        val GRAMMAR_SPANISH = "GRAMMAR_SPANISH"
        val GAME = "GAME"
    }
}