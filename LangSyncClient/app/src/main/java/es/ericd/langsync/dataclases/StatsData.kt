package es.ericd.langsync.dataclases

import com.google.firebase.Timestamp

data class StatItem(
    var isCorrect: Boolean,
    var toGet: String,
    var userInput: String

)

data class StatsPartyData(
    var timestamp: Timestamp,
    var partyCode: String,
    var position: Long,
    var data: List<StatItem>
)

data class StatsData(
    var points: Long,
    var partyData: MutableList<StatsPartyData>
)
