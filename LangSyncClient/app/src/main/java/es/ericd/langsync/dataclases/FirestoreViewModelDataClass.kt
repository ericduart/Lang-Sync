package es.ericd.langsync.dataclases

data class FirestoreViewModelDataClass(
    val partyCode: String,
    val gameStars: Boolean,
    val grammar: HashMap<String, String>
)
