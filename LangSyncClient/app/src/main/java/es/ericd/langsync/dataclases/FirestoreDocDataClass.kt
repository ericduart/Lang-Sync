package es.ericd.langsync.dataclases


data class FirestoreDocDataClass(
    val players: List<String> = emptyList(),
    val grammar: HashMap<String, String> = hashMapOf(),
    val grammarList: List<HashMap<String, String>> = emptyList(),
    val gameEnded: Boolean = false
)
