package es.ericd.langsync.fragments.dashboard

import android.content.Context
import android.os.Bundle
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.lifecycle.ViewModelProvider
import androidx.lifecycle.lifecycleScope
import com.google.android.material.snackbar.Snackbar
import es.ericd.langsync.R
import es.ericd.langsync.databinding.FragmentMenuBinding
import es.ericd.langsync.fragments.dialogs.PlayPrivateGameDialogFragment
import es.ericd.langsync.fragments.logins.LoginFragment
import es.ericd.langsync.services.FirestoreService
import es.ericd.langsync.viewModels.FirestoreViewModel
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch
import kotlinx.coroutines.withContext


class MenuFragment : Fragment() {

    lateinit var binding: FragmentMenuBinding
    var menuInterface: MenuFragmentInterface? = null

    override fun onAttach(context: Context) {
        super.onAttach(context)

        if (context is MenuFragmentInterface) {
            menuInterface = context
        } else {
            throw Exception("Activity must implement LoginFragmentInterface")
        }

    }

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {

        binding = FragmentMenuBinding.inflate(inflater)

        val joinParty = { partyCode: String ->
            Snackbar.make(binding.root, partyCode, Snackbar.LENGTH_LONG).show()

            lifecycleScope.launch(Dispatchers.IO) {
                val joined = FirestoreService.joinPrivateGame(partyCode)

                withContext(Dispatchers.Main) {
                    //Snackbar.make(binding.root, , Snackbar.LENGTH_LONG).show()
                    if (!joined) {
                        Snackbar.make(binding.root, getString(R.string.error_joining_private_game), Snackbar.LENGTH_LONG).show()
                        return@withContext
                    }

                    menuInterface?.openPreGame(partyCode)

                }

            }

        }

        binding.btnPlay.setOnClickListener {
            PlayPrivateGameDialogFragment(joinParty).show(requireActivity().supportFragmentManager, "GAME_DIALOG")
        }

        return binding.root
    }

    interface MenuFragmentInterface {
        fun openPreGame(partyGame: String)
    }

}