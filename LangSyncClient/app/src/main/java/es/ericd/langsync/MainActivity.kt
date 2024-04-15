package es.ericd.langsync

import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import androidx.core.os.bundleOf
import androidx.fragment.app.commit
import androidx.fragment.app.replace
import es.ericd.langsync.databinding.ActivityMainBinding
import es.ericd.langsync.fragments.dashboard.MenuFragment
import es.ericd.langsync.fragments.dashboard.PregameFragment
import es.ericd.langsync.fragments.logins.LoginFragment
import es.ericd.langsync.fragments.logins.SignupFragment

class MainActivity : AppCompatActivity(), MenuFragment.MenuFragmentInterface {

    lateinit var binding: ActivityMainBinding

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)

        binding = ActivityMainBinding.inflate(layoutInflater)

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

}