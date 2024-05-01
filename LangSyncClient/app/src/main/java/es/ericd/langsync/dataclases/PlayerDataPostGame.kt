package es.ericd.langsync.dataclases

data class PlayerInputs(
    val english: String,
    val spanish: String,
    val isCorrect: Boolean
)

data class PlayerDataPostGame(
    val userName: String,
    val inputs: List<PlayerInputs>
)
