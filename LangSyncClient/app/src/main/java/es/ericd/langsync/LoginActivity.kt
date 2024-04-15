package es.ericd.langsync

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.view.View
import androidx.fragment.app.commit
import androidx.fragment.app.replace
import androidx.lifecycle.lifecycleScope
import com.google.android.material.snackbar.Snackbar
import es.ericd.langsync.fragments.logins.LoginFragment
import es.ericd.langsync.fragments.logins.SignupFragment
import es.ericd.langsync.services.FirebaseAuth
import es.ericd.langsync.utils.Utils
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch
import kotlinx.coroutines.withContext

class LoginActivity : AppCompatActivity(), LoginFragment.LoginFragmentInterface, SignupFragment.SignupFragmentInteface {

    lateinit var rootView: View

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        rootView = findViewById(android.R.id.content)
        setContentView(R.layout.activity_login)
    }

    override fun openSignUpFragment() {
        supportFragmentManager.commit {
            setReorderingAllowed(true)
            addToBackStack(null)
            replace<SignupFragment>(R.id.fragment_container_view_tag)
        }
    }

    override fun login(email: String, pwd: String) {

        lifecycleScope.launch(Dispatchers.IO) {

            try {
                FirebaseAuth.logIn(email, pwd)

                withContext(Dispatchers.Main) {

                    val intent = Intent(this@LoginActivity, MainActivity::class.java)

                    startActivity(intent)
                    finish()

                }
            } catch (e: Exception) {

                withContext(Dispatchers.Main) {

                    if (!Utils.isInternetAvailable(this@LoginActivity)) {
                        Snackbar.make(rootView, "Internet is requiered.", Snackbar.LENGTH_LONG).show()
                    } else {
                        Snackbar.make(rootView, "Something unexpected happend", Snackbar.LENGTH_LONG).show()

                    }


                }
            }

        }
    }

    override fun signup(email: String, displayName: String, pwd: String) {

        lifecycleScope.launch(Dispatchers.IO) {

            try {
                FirebaseAuth.registerUser(email, displayName, pwd)

                withContext(Dispatchers.Main) {
                    Snackbar.make(rootView, "holaaa", Snackbar.LENGTH_LONG).show()
                }

            } catch (e: Exception) {
                withContext(Dispatchers.Main) {
                    val rootView: View = findViewById(android.R.id.content)
                    Snackbar.make(rootView, "hola -> " + e.message, Snackbar.LENGTH_LONG).show()
                }
            }


        }

    }
}