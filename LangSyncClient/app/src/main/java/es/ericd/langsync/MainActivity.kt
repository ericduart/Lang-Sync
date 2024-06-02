package es.ericd.langsync

import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.util.Log
import androidx.core.os.bundleOf
import androidx.core.view.get
import androidx.fragment.app.commit
import androidx.fragment.app.replace
import androidx.lifecycle.ViewModelProvider
import es.ericd.langsync.databinding.ActivityMainBinding
import es.ericd.langsync.fragments.dashboard.GameFragment
import es.ericd.langsync.fragments.dashboard.MenuFragment
import es.ericd.langsync.fragments.dashboard.PostGameFragment
import es.ericd.langsync.fragments.dashboard.PregameFragment
import es.ericd.langsync.fragments.dashboard.StatsFragment
import es.ericd.langsync.fragments.logins.LoginFragment
import es.ericd.langsync.fragments.logins.SignupFragment
import es.ericd.langsync.services.FirestoreService
import es.ericd.langsync.viewModels.FirestoreViewModel

class MainActivity : AppCompatActivity(), MenuFragment.MenuFragmentInterface, GameFragment.GameFragmentInterface, PostGameFragment.PostGameInterface {

    lateinit var binding: ActivityMainBinding
    lateinit var viewModel: FirestoreViewModel

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)

        FirestoreService.initViewModel(this)
        viewModel = ViewModelProvider(this).get(FirestoreViewModel::class.java)
        binding = ActivityMainBinding.inflate(layoutInflater)

        viewModel.gameStarts.observe(this) { newData ->

            if (newData.gameStars) {

                val mbundle = bundleOf(
                    GameFragment.PARTY_CODE to newData.partyCode,
                    GameFragment.GRAMMAR_ENGLISH to newData.grammar.get("english"),
                    GameFragment.GRAMMAR_SPANISH to newData.grammar.get("spanish")
                )

                supportFragmentManager.commit {
                    addToBackStack(null)
                    replace<GameFragment>(binding.fragmentContainerView.id, args = mbundle)
                    setReorderingAllowed(true)
                }
            }

        }

        setContentView(binding.root)
    }

    override fun openPreGame(partyCode: String) {

        val mbundle = bundleOf(PregameFragment.PARTY_CODE to partyCode)

        supportFragmentManager.commit {
            addToBackStack(null)
            replace<PregameFragment>(binding.fragmentContainerView.id, args = mbundle)
            setReorderingAllowed(true)
        }
    }

    override fun getStats() {
        supportFragmentManager.commit {
            addToBackStack(null)
            replace<StatsFragment>(binding.fragmentContainerView.id)
            setReorderingAllowed(true)
        }
    }

    override fun showEndingGameFragment(partyCode: String) {
        val mbundle = bundleOf(PregameFragment.PARTY_CODE to partyCode)

        supportFragmentManager.commit {
            addToBackStack(null)
            replace<PostGameFragment>(binding.fragmentContainerView.id, args = mbundle)
            setReorderingAllowed(true)
        }
    }

    override fun goBackToMenu() {
        supportFragmentManager.commit {
            replace<MenuFragment>(binding.fragmentContainerView.id)
            setReorderingAllowed(true)
        }
    }

}