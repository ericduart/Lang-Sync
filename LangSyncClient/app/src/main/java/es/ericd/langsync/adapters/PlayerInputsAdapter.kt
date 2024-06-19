package es.ericd.langsync.adapters

import android.content.Context
import android.graphics.Color
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.TextView
import androidx.recyclerview.widget.RecyclerView
import es.ericd.langsync.R
import es.ericd.langsync.dataclases.FirestorePlayersChosesDataClass
import es.ericd.langsync.dataclases.FirestorePlayersDataGrammar

class PlayerInputsAdapter(val context: Context, val inputs: MutableList<FirestorePlayersDataGrammar>): RecyclerView.Adapter<PlayerInputsAdapter.PlayerInputsViewHolder>() {
    class PlayerInputsViewHolder(view: View): RecyclerView.ViewHolder(view) {

        private val tvEnglish: TextView = view.findViewById(R.id.tvEnglish)
        private val tvInput: TextView = view.findViewById(R.id.tvInput)
        private val tvIsCorrect: TextView = view.findViewById(R.id.tvIsCorrect)

        fun bindItem(item: FirestorePlayersDataGrammar) {
            tvEnglish.text = item.name
            tvInput.text = item.userInput
            tvIsCorrect.text = item.isCorrect.toString()
            // if item is correct print it green
            if (item.isCorrect) {
                tvIsCorrect.setTextColor(Color.GREEN)
            } else {
                tvIsCorrect.setTextColor(Color.RED)
            }

        }
    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): PlayerInputsViewHolder {
        val view = LayoutInflater.from(context).inflate(R.layout.player_input_item, parent, false)

        return PlayerInputsViewHolder(view)

    }

    override fun getItemCount(): Int {
        return inputs.size
    }

    override fun onBindViewHolder(holder: PlayerInputsViewHolder, position: Int) {
        holder.bindItem(inputs[position])
    }
}