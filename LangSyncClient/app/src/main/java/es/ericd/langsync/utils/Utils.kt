package es.ericd.langsync.utils

import android.content.Context
import android.net.ConnectivityManager
import android.net.NetworkCapabilities
import android.os.Build
import java.util.Dictionary

class Utils {
    companion object {
        fun isInternetAvailable(context: Context): Boolean {
            var result: Boolean
            val connectivityManager = context.getSystemService(Context.CONNECTIVITY_SERVICE) as ConnectivityManager
            val networkCapabilities = connectivityManager.activeNetwork ?: return false
            val actNw = connectivityManager.getNetworkCapabilities(networkCapabilities) ?: return false
            result = when {
                actNw.hasTransport(NetworkCapabilities.TRANSPORT_WIFI) -> true
                actNw.hasTransport(NetworkCapabilities.TRANSPORT_CELLULAR) -> true
                actNw.hasTransport(NetworkCapabilities.TRANSPORT_ETHERNET) -> true
                else -> false
            }

            return result

        }

        fun getPostionsPoints(): Map<Int, Int> {
            val points = mapOf<Int, Int>(
                1 to 10,
                2 to 8,
                3 to 6,
                4 to 4,
                5 to 2

            )

            return points
        }
    }
}