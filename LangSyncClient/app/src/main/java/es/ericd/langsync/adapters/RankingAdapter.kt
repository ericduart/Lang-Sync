package es.ericd.langsync.adapters

import android.content.Context
import android.opengl.Visibility
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.LinearLayout
import android.widget.TextView
import androidx.recyclerview.widget.RecyclerView
import es.ericd.langsync.R
import es.ericd.langsync.dataclases.FirestorePlayersChosesDataClass
import es.ericd.langsync.dataclases.FirestorePlayersDataGrammar
import es.ericd.langsync.dataclases.PlayerDataPostGame

class RankingAdapter(val context: Context, val rankingItems: MutableList<FirestorePlayersChosesDataClass>, val clickItem: (inputs: List<FirestorePlayersDataGrammar>) -> Unit?): RecyclerView.Adapter<RankingAdapter.RankingViewHolder>() {

    class RankingViewHolder(view: View): RecyclerView.ViewHolder(view) {

        private val tvUser: TextView = view.findViewById(R.id.tvRankingUser)
        private val tvRanking: TextView = view.findViewById(R.id.tvRankingPosition)
        private val layoutPoints: LinearLayout = view.findViewById(R.id.layoutPoints)

        fun bindItem(position: String, item: FirestorePlayersChosesDataClass) {
            tvUser.text = item.user
            tvRanking.text = "#${position}"
            layoutPoints.visibility = View.GONE

        }
    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): RankingViewHolder {
        val view = LayoutInflater.from(context).inflate(R.layout.ranking_item, parent, false)

        return RankingViewHolder(view)

    }

    override fun getItemCount(): Int {
        return rankingItems.size
    }

    override fun onBindViewHolder(holder: RankingViewHolder, position: Int) {

        val item = rankingItems[position]

        holder.bindItem((position + 1).toString(),item)
        holder.itemView.setOnClickListener { clickItem(item.grammar) }
    }

}