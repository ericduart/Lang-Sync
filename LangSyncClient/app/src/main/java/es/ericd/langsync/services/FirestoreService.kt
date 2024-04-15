package es.ericd.langsync.services

import android.util.Log
import com.google.firebase.firestore.DocumentId
import com.google.firebase.firestore.DocumentReference
import com.google.firebase.firestore.FieldPath
import com.google.firebase.firestore.FirebaseFirestore
import com.google.firebase.firestore.ListenerRegistration
import kotlinx.coroutines.channels.awaitClose
import kotlinx.coroutines.flow.callbackFlow
import kotlinx.coroutines.tasks.await
import java.lang.IllegalArgumentException
import kotlinx.coroutines.flow.Flow

class FirestoreService {
    companion object {

        private var firebaseDB : FirebaseFirestore? = null
        private var subscription: ListenerRegistration? = null

        private fun getInstance(): FirebaseFirestore {
            if (firebaseDB == null) {
                firebaseDB = FirebaseFirestore.getInstance()
            }

            return firebaseDB!!

        }

        suspend fun joinPrivateGame(partyCode: String): Boolean {

            try {

                val firestore = getInstance()

                val doc = firestore.collection("parties").document(partyCode)

                val snapshot = doc.get().await()

                if (!snapshot.exists()) return false

                // Get the current list of players
                val players = snapshot.get("players") as? List<String> ?: emptyList<String>()

                // Add the new player to the list
                val updatedPlayers = players.toMutableList()
                updatedPlayers.add(FirebaseAuth.getCurrentUser()?.displayName!!)

                // Update the "players" field in the document
                val updates = hashMapOf<String, Any>(
                    "players" to updatedPlayers
                )

                // Perform the update
                doc.update(updates).await()

                return true
            } catch (e: Exception) {
                return false
            }

        }

        fun getPartyReference(partyCode: String): DocumentReference {
            val firestore = getInstance()
            return firestore.collection("parties").document(partyCode)
        }

        suspend fun getDocPlayersChanges(partyCode: String) : Flow<List<String>?> = callbackFlow{
            try {
                val partiesCollection = getInstance().collection("parties").document(partyCode)

                subscription = partiesCollection.addSnapshotListener { snapshot, err ->

                    if (snapshot != null && snapshot.exists()) {

                        Log.d("listener","ara")

                        val players = snapshot.get("players") as? List<String>

                        trySend(players)

                    }


                }

                awaitClose { subscription?.remove() }

            } catch (e: Throwable) {
                close(e)
            }
        }

        suspend fun leaveGame(partyCode: String): Boolean {
            val docRef = getInstance().collection("parties").document(partyCode)

            val snapshot = docRef.get().await()
            val user = FirebaseAuth.getCurrentUser()

            if (!snapshot.exists() || user == null) return false

            val players = snapshot.get("players") as? List<String> ?: emptyList<String>()

            // Add the new player to the list
            val updatedPlayers = players.toMutableList()
            updatedPlayers.removeAll{ p -> p.equals(user.displayName)}

            // Update the "players" field in the document
            val updates = hashMapOf<String, Any>(
                "players" to updatedPlayers
            )

            // Perform the update
            docRef.update(updates).await()

            return true


        }

    }
}