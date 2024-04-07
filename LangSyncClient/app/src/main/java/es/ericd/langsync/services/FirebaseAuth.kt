package es.ericd.langsync.services

import com.google.firebase.auth.FirebaseAuth
import com.google.firebase.auth.FirebaseUser
import com.google.firebase.auth.UserProfileChangeRequest
import com.google.firebase.auth.ktx.auth
import com.google.firebase.ktx.Firebase
import kotlinx.coroutines.tasks.await

class FirebaseAuth {

    companion object {

        private val auth: FirebaseAuth by lazy { Firebase.auth }

        suspend fun logIn(email: String, password: String): FirebaseUser? {
            val authResult = auth.signInWithEmailAndPassword(email, password).await()
            return authResult.user
        }

        fun logOut() = auth.signOut()

        fun getCurrentUser(): FirebaseUser? = auth.currentUser

        suspend fun registerUser(email: String, displayName: String, password: String): FirebaseUser? {
            val authResult = auth.createUserWithEmailAndPassword(email, password).await()

            val updateProfileBuilder = UserProfileChangeRequest.Builder().setDisplayName(displayName).build()

            authResult.user!!.updateProfile(updateProfileBuilder).await()

            return authResult.user
        }

        suspend fun changeDisplayName(newDisplayName: String) {
            val user = getCurrentUser() ?: throw Exception("User is null")

            val updateProfileBuilder = UserProfileChangeRequest.Builder().setDisplayName(newDisplayName).build()

            user.updateProfile(updateProfileBuilder).await()
        }

        fun resetPassword(email: String) {
            auth.sendPasswordResetEmail(email)
        }

    }

}