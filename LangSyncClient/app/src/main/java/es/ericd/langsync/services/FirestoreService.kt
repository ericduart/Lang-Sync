package es.ericd.langsync.services

import android.util.Log
import androidx.lifecycle.ViewModelProvider
import androidx.lifecycle.ViewModelStoreOwner
import com.google.firebase.Timestamp
import com.google.firebase.firestore.DocumentReference
import com.google.firebase.firestore.FieldValue
import com.google.firebase.firestore.FirebaseFirestore
import com.google.firebase.firestore.ListenerRegistration
import com.google.firebase.firestore.SetOptions
import com.google.firebase.firestore.snapshots
import es.ericd.langsync.dataclases.FirestoreDocDataClass
import es.ericd.langsync.dataclases.FirestorePlayersChosesDataClass
import es.ericd.langsync.dataclases.FirestorePlayersDataGrammar
import es.ericd.langsync.dataclases.FirestoreViewModelDataClass
import es.ericd.langsync.dataclases.PlayerDataPostGame
import es.ericd.langsync.dataclases.PlayerInputs
import es.ericd.langsync.dataclases.PlayerPoints
import es.ericd.langsync.dataclases.StatItem
import es.ericd.langsync.dataclases.StatsData
import es.ericd.langsync.dataclases.StatsPartyData
import es.ericd.langsync.fragments.dashboard.PregameFragment
import es.ericd.langsync.utils.Utils
import es.ericd.langsync.viewModels.FirestoreViewModel
import kotlinx.coroutines.channels.awaitClose
import kotlinx.coroutines.flow.callbackFlow
import kotlinx.coroutines.tasks.await
import kotlinx.coroutines.flow.Flow

class FirestoreService {

    companion object {

        private var firebaseDB : FirebaseFirestore? = null
        private var subscription: ListenerRegistration? = null
        private var mViewModel: FirestoreViewModel? = null

        private fun getInstance(): FirebaseFirestore {
            if (firebaseDB == null) {
                firebaseDB = FirebaseFirestore.getInstance()
            }

            return firebaseDB!!

        }

        fun initViewModel(viewModelStoreOwner: ViewModelStoreOwner) {
            mViewModel = ViewModelProvider(viewModelStoreOwner).get(FirestoreViewModel::class.java)
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

        suspend fun getDocPlayersChanges(partyCode: String, comeFrom: String = "") : Flow<FirestoreDocDataClass> = callbackFlow{
            try {
                val partiesCollection = getInstance().collection("parties").document(partyCode)

                subscription = partiesCollection.addSnapshotListener { snapshot, err ->

                    if (snapshot != null && snapshot.exists()) {

                        val currentGrammar = snapshot.get("currentGrammar") as HashMap<String, String>

                        // Check if game starts
                        if (!currentGrammar.get("english").equals("") && comeFrom.equals(PregameFragment.PREGAME)) {
                            mViewModel?.gameStarts?.value = FirestoreViewModelDataClass(partyCode = partyCode, gameStars = true, currentGrammar)
                            return@addSnapshotListener
                        }

                        val players = snapshot.get("players") as? List<String>
                        val listOfGrammar = snapshot.get("grammar") as? List<HashMap<String, String>> ?: emptyList()

                        val gameEnded = snapshot.get("gameEnded") as? Boolean ?: false

                        trySend(
                            FirestoreDocDataClass(
                                players = players ?: emptyList(),
                                grammar = currentGrammar,
                                grammarList = listOfGrammar,
                                gameEnded = gameEnded

                            )
                        )

                    }

                }

                awaitClose { subscription?.remove() }

            } catch (e: Throwable) {
                close(e)
            }
        }

        suspend fun getCollectionPlayersDataChanges(partyCode: String): Flow<List<FirestorePlayersChosesDataClass>> = callbackFlow {

            try {
                val partiesCollection = getInstance().collection("parties").document(partyCode).collection("playersData")

                val subscription = partiesCollection.addSnapshotListener{ snapshot, err ->
                    val playersData = mutableListOf<FirestorePlayersChosesDataClass>()
                    snapshot?.forEach { it ->

                        val grammarData = mutableListOf<FirestorePlayersDataGrammar>()

                        val grammarPerPlayer = it.data.toMap()

                        for ((key, value) in grammarPerPlayer) {
                            val map = (value as? Map<*,*>)?.toMap() ?: emptyMap()

                            grammarData.add(FirestorePlayersDataGrammar(name = key, userInput = map["input"] as String, isCorrect = map["isCorrect"] as Boolean))
                        }

                        playersData.add(FirestorePlayersChosesDataClass(user = it.id, grammar = grammarData))

                    }

                    trySend(playersData)
                }

                awaitClose { subscription?.remove() }

            } catch (e: Throwable) {
                close(e)
            }

        }

        suspend fun setPlayerGrammar(partyCode: String, grammarList: List<HashMap<String, String>>, grammarSpanish: String, grammarEnglish: String ) {
            try {
                // TODO: grammarList pa saber si el input del user es correcte

                val firebase = getInstance()
                var alreadySend = false

                val currentUserName = FirebaseAuth.getCurrentUser()!!.displayName!!
                val doc = firebase
                    .collection("parties")
                    .document(partyCode)
                    .collection("playersData")
                    .document(currentUserName)

                val snapshot = doc.get().await()

                val snapshotExists = snapshot.exists()

                if (snapshotExists) {

                    alreadySend = snapshot.data?.keys?.any { it.equals(grammarEnglish) } ?: false

                }

                if (alreadySend) return

                val findedGrammarList = grammarList.find { it.get("english")?.equals(grammarEnglish) ?: false }

                if (findedGrammarList != null) {
                    val correct = findedGrammarList.get("english").equals(grammarEnglish) && findedGrammarList.get("spanish").equals(grammarSpanish)

                    if (!snapshotExists) {
                        doc.set(hashMapOf(
                            grammarEnglish to hashMapOf("input" to grammarSpanish, "isCorrect" to correct)
                        )).await()

                    } else {
                        doc.update(
                            hashMapOf(
                                grammarEnglish to hashMapOf("input" to grammarSpanish, "isCorrect" to correct)
                            ) as Map<String, Any>
                        ).await()
                    }
                }
            } catch (e: Exception) {
                Log.d("setPlayerGrammar",e.message.toString())
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

        suspend fun getPlayersDataAfterGame(partyCode: String): MutableList<FirestorePlayersChosesDataClass> {
            val collRef = getInstance().collection("parties").document(partyCode).collection("playersData")

            val snapshot = collRef.get().await()
            val list = mutableListOf<FirestorePlayersChosesDataClass>()

            snapshot.documents.forEach {
                val userInputs = mutableListOf<FirestorePlayersDataGrammar>()

                it.data?.forEach {
                    val inputData = it.value as Map<*, *>

                    userInputs.add(
                        FirestorePlayersDataGrammar(
                            name = it.key,
                            userInput = inputData.get("input") as? String ?: "",
                            isCorrect = inputData.get("isCorrect") as? Boolean ?: false
                        )
                    )

                }

                list.add(FirestorePlayersChosesDataClass(
                    user = it.id,
                    grammar = userInputs
                ))

            }

            list.sortByDescending  {it.grammar.count{ it.isCorrect }}

            return list

        }

        suspend fun saveStats(partyCode: String, p: FirestorePlayersChosesDataClass, playerPosition: Int): Boolean {

            try {

                val points = Utils.getPostionsPoints()

                val playerPoints = points.get(playerPosition) ?: 0

                var doc = getInstance().collection("stats").document(FirebaseAuth.getCurrentUser()?.email!!)

                var data = hashMapOf(
                    partyCode to hashMapOf(
                        "data" to p.grammar,
                        "position" to playerPosition,
                        "timestamp" to Timestamp.now()
                    ),
                    "displayName" to FirebaseAuth.getCurrentUser()?.displayName!!
                )

                doc.set(data, SetOptions.merge()).await()
                doc.update("points", FieldValue.increment(playerPoints.toLong())).await()

                return true
            } catch (e: Exception) {
                return false
            }

        }

        suspend fun getStats(): StatsData {

            val stats = StatsData(
                points = 0,
                partyData = mutableListOf<StatsPartyData>()
            )

            try {
                var doc = getInstance().collection("stats").document(FirebaseAuth.getCurrentUser()?.email!!)

                var data = doc.get().await()

                stats.points = data.get("points") as Long? ?: 0

                data.data?.forEach {

                    if (!it.key.equals("points") && !it.key.equals("displayName")) {
                        val partyData = it.value as HashMap<*, *>
                        val partyCode = it.key

                        val position = partyData.get("position") as Long
                        val timestamp = partyData.get("timestamp") as Timestamp

                        val items = partyData.get("data") as List<HashMap<*, *>>



                        val statsItems = mutableListOf<StatItem>()

                        items.forEach {
                            statsItems.add(
                                StatItem(
                                    isCorrect = it.get("correct") as Boolean,
                                    toGet = it.get("name") as String,
                                    userInput = it.get("userInput") as String
                                )
                            )
                        }

                        val partyStats =
                            StatsPartyData(
                                position = position,
                                timestamp = timestamp,
                                partyCode = partyCode,
                                data = statsItems
                            )


                        stats.partyData.add(partyStats)
                    }
                }
            } catch (e: Exception) {
                return stats
            }

            return stats
        }

        suspend fun getAllPlayersPoints(): MutableList<PlayerPoints> {
            var playersCollection = getInstance().collection("stats")

            var list = mutableListOf<PlayerPoints>()

            val snapshots = playersCollection.get().await()

            snapshots.documents.forEach{
                list.add(PlayerPoints(
                    player = it.get("displayName") as String? ?: it.id,
                    points = it.get("points") as Long? ?: 0
                ))
            }

            list = list.sortedByDescending { it.points }.toMutableList()

            return list


        }

    }
    interface firestoreServiceInterface {
        fun startGame(partyCode: String)
    }
}