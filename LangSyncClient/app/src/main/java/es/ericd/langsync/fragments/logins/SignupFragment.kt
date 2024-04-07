package es.ericd.langsync.fragments.logins

import android.content.Context
import android.os.Bundle
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Toast
import com.google.android.material.snackbar.Snackbar
import es.ericd.langsync.R
import es.ericd.langsync.databinding.FragmentSignupBinding


class SignupFragment : Fragment() {

    lateinit var binding: FragmentSignupBinding
    var signupInterface: SignupFragmentInteface? = null

    override fun onAttach(context: Context) {
        super.onAttach(context)

        if (context is SignupFragmentInteface) {
            signupInterface = context
        } else {
            throw Exception("Activity must implement SignupFragmentInteface")
        }
    }

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        // Inflate the layout for this fragment
        binding = FragmentSignupBinding.inflate(inflater)

        binding.button.setOnClickListener { signup() }

        return binding.root
    }

    fun signup() {
        val email = binding.etEmail.text.toString()
        val displayName = binding.etName.text.toString()
        val pwd = binding.etPwd.text.toString()
        val pwd2 = binding.etPwd2.text.toString()


        if (email.isNullOrEmpty()) {
            Snackbar.make(binding.root, "Email is required",Snackbar.LENGTH_SHORT).show()
            return
        }

        if (displayName.isNullOrEmpty()) {
            Snackbar.make(binding.root, "Name is required", Snackbar.LENGTH_SHORT).show()
            return
        }

        if (pwd.isNullOrEmpty()) {
            Snackbar.make(binding.root, "Password is required", Snackbar.LENGTH_SHORT).show()
            return
        }

        if (pwd.length < 6) {
            Snackbar.make(binding.root, "Password must have at least 6 characters.", Snackbar.LENGTH_SHORT).show()
            return
        }

        if (!pwd.equals(pwd2)) {
            Snackbar.make(binding.root, "Passwords doesn't match", Snackbar.LENGTH_SHORT).show()
            return
        }

        signupInterface?.signup(email, displayName, pwd)

    }

    interface SignupFragmentInteface {
        fun signup(email: String, displayName: String, pwd: String)
    }

}