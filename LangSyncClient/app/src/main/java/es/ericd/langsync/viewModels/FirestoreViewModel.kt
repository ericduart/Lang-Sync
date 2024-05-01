package es.ericd.langsync.viewModels

import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.ViewModel
import es.ericd.langsync.dataclases.FirestoreViewModelDataClass

class FirestoreViewModel: ViewModel() {
    val gameStarts = MutableLiveData<FirestoreViewModelDataClass>()

}