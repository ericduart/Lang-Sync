package es.ericd.langsync.fragments.dialogs

import android.app.AlertDialog
import android.app.Dialog
import android.os.Bundle
import android.widget.TextView
import androidx.fragment.app.DialogFragment
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import es.ericd.langsync.R
import es.ericd.langsync.adapters.PlayerInputsAdapter
import es.ericd.langsync.dataclases.FirestorePlayersDataGrammar

class ShowDataPostGameDialogFragment(val user: String, val inputs: MutableList<FirestorePlayersDataGrammar>): DialogFragment() {
    override fun onCreateDialog(savedInstanceState: Bundle?): Dialog {
        return activity?.let {
            val builder = AlertDialog.Builder(it)

            val view = requireActivity().layoutInflater.inflate(R.layout.dialog_show_data_post_game, null)

            val recView = view.findViewById<RecyclerView>(R.id.recView)

            recView.adapter = PlayerInputsAdapter(requireContext(), inputs)

            recView.layoutManager = LinearLayoutManager(requireContext(), LinearLayoutManager.VERTICAL, false)


            view.findViewById<TextView>(R.id.tvUser).text = user.uppercase()

            builder.setView(view)

            builder.create()

        }  ?: throw IllegalStateException("Activity cannot be null")
    }
}