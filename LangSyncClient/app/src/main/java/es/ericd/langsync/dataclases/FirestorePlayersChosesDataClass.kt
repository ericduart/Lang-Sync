package es.ericd.langsync.dataclases

data class FirestorePlayersDataGrammar (
    val name: String,
    val userInput: String,
    val isCorrect: Boolean
)

data class FirestorePlayersChosesDataClass(
    val user: String,
    val grammar: List<FirestorePlayersDataGrammar>
)
