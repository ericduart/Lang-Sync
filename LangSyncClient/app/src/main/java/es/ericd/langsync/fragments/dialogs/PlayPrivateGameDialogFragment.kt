package es.ericd.langsync.fragments.dialogs

import android.app.AlertDialog
import android.app.Dialog
import android.os.Bundle
import android.widget.EditText
import androidx.fragment.app.DialogFragment
import es.ericd.langsync.R
import kotlinx.coroutines.Job

class PlayPrivateGameDialogFragment(val joinGame: (partyCode: String) -> Job): DialogFragment() {
    override fun onCreateDialog(savedInstanceState: Bundle?): Dialog {
        return activity?.let {
            val builder = AlertDialog.Builder(it)

            //val inflater = requireActivity().layoutInflater
            val view = requireActivity().layoutInflater.inflate(R.layout.dialog_privateparty, null)

            builder
                .setView(view)
                .setPositiveButton("Play") { dialog, id ->
                    val partyCode = view.findViewById<EditText>(R.id.etPartyCode).text.toString()

                    joinGame(partyCode)

                }
                .setNegativeButton("Back") { dialog, id -> dialog.cancel() }

            builder.create()


        } ?: throw IllegalStateException("Activity cannot be null")
    }
}