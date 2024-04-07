package es.ericd.langsync.fragments.logins

import android.content.Context
import android.os.Bundle
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import es.ericd.langsync.databinding.FragmentLoginBinding
import android.text.SpannableString
import android.text.style.UnderlineSpan


class LoginFragment : Fragment() {

    lateinit var binding: FragmentLoginBinding
    var loginInterface: LoginFragmentInterface? = null

    override fun onAttach(context: Context) {
        super.onAttach(context)

        if (context is LoginFragmentInterface) {
            loginInterface = context
        } else {
            throw Exception("Activity must implement LoginFragmentInterface")
        }

    }

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        // Inflate the layout for this fragment
        binding = FragmentLoginBinding.inflate(inflater)

        val mSpannableString = SpannableString(binding.btnForgotPasswd.text)
        mSpannableString.setSpan(UnderlineSpan(), 0, mSpannableString.length, 0)
        binding.btnForgotPasswd.text = mSpannableString

        binding.btnForgotPasswd.setOnClickListener { loginInterface?.openSignUpFragment() }

        binding.button.setOnClickListener {
            val email = binding.etEmail.text.toString()
            val pwd = binding.etPwd.text.toString()

            loginInterface?.login(email, pwd)

        }

        return binding.root
    }

    interface LoginFragmentInterface {
        fun openSignUpFragment()
        fun login(email: String, pwd: String)
    }

}