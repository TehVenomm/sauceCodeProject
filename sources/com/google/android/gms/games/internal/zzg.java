package com.google.android.gms.games.internal;

import android.net.Uri;
import android.os.Bundle;
import android.os.Parcel;
import android.os.RemoteException;
import com.google.android.gms.common.api.Status;
import com.google.android.gms.common.data.DataHolder;
import com.google.android.gms.drive.zzc;
import com.google.android.gms.games.GamesActivityResultCodes;
import com.google.android.gms.games.GamesStatusCodes;
import com.google.android.gms.games.multiplayer.realtime.RealTimeMessage;
import com.google.android.gms.games.video.VideoCapabilities;
import com.google.android.gms.internal.zzef;
import com.google.android.gms.internal.zzeg;
import com.google.android.gms.nearby.connection.ConnectionsStatusCodes;
import net.gogame.chat.ChatFragment;

public abstract class zzg extends zzef implements zzf {
    public zzg() {
        attachInterface(this, "com.google.android.gms.games.internal.IGamesCallbacks");
    }

    public boolean onTransact(int i, Parcel parcel, Parcel parcel2, int i2) throws RemoteException {
        if (zza(i, parcel, parcel2, i2)) {
            return true;
        }
        switch (i) {
            case ChatFragment.PICK_IMAGE_RESULT_CODE /*5001*/:
                zzg(parcel.readInt(), parcel.readString());
                break;
            case 5002:
                zzf((DataHolder) zzeg.zza(parcel, DataHolder.CREATOR));
                break;
            case 5003:
                zzh(parcel.readInt(), parcel.readString());
                break;
            case 5004:
                zzh((DataHolder) zzeg.zza(parcel, DataHolder.CREATOR));
                break;
            case 5005:
                zza((DataHolder) zzeg.zza(parcel, DataHolder.CREATOR), (DataHolder) zzeg.zza(parcel, DataHolder.CREATOR));
                break;
            case 5006:
                zzi((DataHolder) zzeg.zza(parcel, DataHolder.CREATOR));
                break;
            case 5007:
                zzj((DataHolder) zzeg.zza(parcel, DataHolder.CREATOR));
                break;
            case 5008:
                zzk((DataHolder) zzeg.zza(parcel, DataHolder.CREATOR));
                break;
            case 5009:
                zzl((DataHolder) zzeg.zza(parcel, DataHolder.CREATOR));
                break;
            case 5010:
                zzeg.zza(parcel, DataHolder.CREATOR);
                break;
            case 5011:
                zzeg.zza(parcel, DataHolder.CREATOR);
                break;
            case 5016:
                zzapt();
                break;
            case 5017:
                zzm((DataHolder) zzeg.zza(parcel, DataHolder.CREATOR));
                break;
            case 5018:
                zzu((DataHolder) zzeg.zza(parcel, DataHolder.CREATOR));
                break;
            case 5019:
                zzv((DataHolder) zzeg.zza(parcel, DataHolder.CREATOR));
                break;
            case 5020:
                onLeftRoom(parcel.readInt(), parcel.readString());
                break;
            case 5021:
                zzw((DataHolder) zzeg.zza(parcel, DataHolder.CREATOR));
                break;
            case 5022:
                zzx((DataHolder) zzeg.zza(parcel, DataHolder.CREATOR));
                break;
            case 5023:
                zzy((DataHolder) zzeg.zza(parcel, DataHolder.CREATOR));
                break;
            case 5024:
                zzz((DataHolder) zzeg.zza(parcel, DataHolder.CREATOR));
                break;
            case 5025:
                zzaa((DataHolder) zzeg.zza(parcel, DataHolder.CREATOR));
                break;
            case 5026:
                zza((DataHolder) zzeg.zza(parcel, DataHolder.CREATOR), parcel.createStringArray());
                break;
            case 5027:
                zzb((DataHolder) zzeg.zza(parcel, DataHolder.CREATOR), parcel.createStringArray());
                break;
            case 5028:
                zzc((DataHolder) zzeg.zza(parcel, DataHolder.CREATOR), parcel.createStringArray());
                break;
            case 5029:
                zzd((DataHolder) zzeg.zza(parcel, DataHolder.CREATOR), parcel.createStringArray());
                break;
            case 5030:
                zze((DataHolder) zzeg.zza(parcel, DataHolder.CREATOR), parcel.createStringArray());
                break;
            case 5031:
                zzf((DataHolder) zzeg.zza(parcel, DataHolder.CREATOR), parcel.createStringArray());
                break;
            case 5032:
                onRealTimeMessageReceived((RealTimeMessage) zzeg.zza(parcel, RealTimeMessage.CREATOR));
                break;
            case 5033:
                zzb(parcel.readInt(), parcel.readInt(), parcel.readString());
                break;
            case 5034:
                parcel.readInt();
                parcel.readString();
                zzeg.zza(parcel);
                break;
            case 5035:
                zzeg.zza(parcel, DataHolder.CREATOR);
                break;
            case 5036:
                parcel.readInt();
                break;
            case 5037:
                zzn((DataHolder) zzeg.zza(parcel, DataHolder.CREATOR));
                break;
            case 5038:
                zzeg.zza(parcel, DataHolder.CREATOR);
                break;
            case 5039:
                zzeg.zza(parcel, DataHolder.CREATOR);
                break;
            case 5040:
                parcel.readInt();
                break;
            case GamesStatusCodes.STATUS_MULTIPLAYER_ERROR_NOT_TRUSTED_TESTER /*6001*/:
                onP2PConnected(parcel.readString());
                break;
            case GamesStatusCodes.STATUS_MULTIPLAYER_ERROR_INVALID_MULTIPLAYER_TYPE /*6002*/:
                onP2PDisconnected(parcel.readString());
                break;
            case 8001:
                zzab((DataHolder) zzeg.zza(parcel, DataHolder.CREATOR));
                break;
            case 8002:
                zzb(parcel.readInt(), (Bundle) zzeg.zza(parcel, Bundle.CREATOR));
                break;
            case 8003:
                zzp((DataHolder) zzeg.zza(parcel, DataHolder.CREATOR));
                break;
            case ConnectionsStatusCodes.STATUS_CONNECTION_REJECTED /*8004*/:
                zzq((DataHolder) zzeg.zza(parcel, DataHolder.CREATOR));
                break;
            case ConnectionsStatusCodes.STATUS_NOT_CONNECTED_TO_ENDPOINT /*8005*/:
                zzr((DataHolder) zzeg.zza(parcel, DataHolder.CREATOR));
                break;
            case 8006:
                zzs((DataHolder) zzeg.zza(parcel, DataHolder.CREATOR));
                break;
            case ConnectionsStatusCodes.STATUS_BLUETOOTH_ERROR /*8007*/:
                zzi(parcel.readInt(), parcel.readString());
                break;
            case ConnectionsStatusCodes.STATUS_ALREADY_HAVE_ACTIVE_STRATEGY /*8008*/:
                zzt((DataHolder) zzeg.zza(parcel, DataHolder.CREATOR));
                break;
            case ConnectionsStatusCodes.STATUS_OUT_OF_ORDER_API_CALL /*8009*/:
                onTurnBasedMatchRemoved(parcel.readString());
                break;
            case 8010:
                onInvitationRemoved(parcel.readString());
                break;
            case GamesStatusCodes.STATUS_VIDEO_UNSUPPORTED /*9001*/:
                zzeg.zza(parcel, DataHolder.CREATOR);
                break;
            case 10001:
                zzo((DataHolder) zzeg.zza(parcel, DataHolder.CREATOR));
                break;
            case GamesActivityResultCodes.RESULT_SIGN_IN_FAILED /*10002*/:
                onRequestRemoved(parcel.readString());
                break;
            case GamesActivityResultCodes.RESULT_LICENSE_FAILED /*10003*/:
                zzac((DataHolder) zzeg.zza(parcel, DataHolder.CREATOR));
                break;
            case GamesActivityResultCodes.RESULT_APP_MISCONFIGURED /*10004*/:
                zzeg.zza(parcel, DataHolder.CREATOR);
                break;
            case GamesActivityResultCodes.RESULT_LEFT_ROOM /*10005*/:
                zzc(parcel.readInt(), (Bundle) zzeg.zza(parcel, Bundle.CREATOR));
                break;
            case GamesActivityResultCodes.RESULT_NETWORK_FAILURE /*10006*/:
                zzeg.zza(parcel, DataHolder.CREATOR);
                break;
            case 11001:
                parcel.readInt();
                zzeg.zza(parcel, Bundle.CREATOR);
                break;
            case 12001:
                zzad((DataHolder) zzeg.zza(parcel, DataHolder.CREATOR));
                break;
            case 12003:
                parcel.readInt();
                zzeg.zza(parcel, Bundle.CREATOR);
                break;
            case 12004:
                zza((DataHolder) zzeg.zza(parcel, DataHolder.CREATOR), (zzc) zzeg.zza(parcel, zzc.CREATOR));
                break;
            case 12005:
                zzae((DataHolder) zzeg.zza(parcel, DataHolder.CREATOR));
                break;
            case 12006:
                zzaf((DataHolder) zzeg.zza(parcel, DataHolder.CREATOR));
                break;
            case 12007:
                zzag((DataHolder) zzeg.zza(parcel, DataHolder.CREATOR));
                break;
            case 12008:
                zzai((DataHolder) zzeg.zza(parcel, DataHolder.CREATOR));
                break;
            case 12011:
                zzg((DataHolder) zzeg.zza(parcel, DataHolder.CREATOR));
                break;
            case 12012:
                zzj(parcel.readInt(), parcel.readString());
                break;
            case 12013:
                zzeg.zza(parcel, DataHolder.CREATOR);
                break;
            case 12014:
                zzah((DataHolder) zzeg.zza(parcel, DataHolder.CREATOR));
                break;
            case 12015:
                parcel.readInt();
                zzeg.zza(parcel, Bundle.CREATOR);
                break;
            case 12016:
                zzeg.zza(parcel, DataHolder.CREATOR);
                break;
            case 12017:
                zza((DataHolder) zzeg.zza(parcel, DataHolder.CREATOR), parcel.readString(), (zzc) zzeg.zza(parcel, zzc.CREATOR), (zzc) zzeg.zza(parcel, zzc.CREATOR), (zzc) zzeg.zza(parcel, zzc.CREATOR));
                break;
            case 13001:
                zzeg.zza(parcel, DataHolder.CREATOR);
                break;
            case 13002:
                parcel.readInt();
                break;
            case 14001:
                parcel.createTypedArray(DataHolder.CREATOR);
                break;
            case 15001:
                zzaj((DataHolder) zzeg.zza(parcel, DataHolder.CREATOR));
                break;
            case 17001:
                zzeg.zza(parcel, DataHolder.CREATOR);
                break;
            case 17002:
                parcel.readInt();
                break;
            case 19001:
                zza(parcel.readInt(), (VideoCapabilities) zzeg.zza(parcel, VideoCapabilities.CREATOR));
                break;
            case 19002:
                zzi(parcel.readInt(), zzeg.zza(parcel));
                break;
            case 19003:
                parcel.readInt();
                zzeg.zza(parcel);
                zzeg.zza(parcel);
                break;
            case 19004:
                parcel.readInt();
                break;
            case 19006:
                zzeg.zza(parcel, DataHolder.CREATOR);
                break;
            case 19007:
                parcel.readInt();
                zzeg.zza(parcel, Bundle.CREATOR);
                break;
            case 19008:
                parcel.readInt();
                break;
            case 19009:
                parcel.readInt();
                break;
            case 19010:
                parcel.readInt();
                break;
            case 20001:
                zzeg.zza(parcel, DataHolder.CREATOR);
                break;
            case 20002:
                zzeg.zza(parcel, DataHolder.CREATOR);
                break;
            case 20003:
                zzeg.zza(parcel, DataHolder.CREATOR);
                break;
            case 20004:
                zzeg.zza(parcel, DataHolder.CREATOR);
                break;
            case 20005:
                zzeg.zza(parcel, DataHolder.CREATOR);
                break;
            case 20006:
                zzeg.zza(parcel, DataHolder.CREATOR);
                break;
            case 20007:
                zzeg.zza(parcel, DataHolder.CREATOR);
                break;
            case 20008:
                zzeg.zza(parcel, DataHolder.CREATOR);
                break;
            case 20009:
                zzeg.zza(parcel, DataHolder.CREATOR);
                break;
            case 20010:
                parcel.readInt();
                parcel.readString();
                parcel.readString();
                break;
            case 20011:
                parcel.readInt();
                parcel.readString();
                break;
            case 20012:
                zzeg.zza(parcel, Status.CREATOR);
                break;
            case 20013:
                zzeg.zza(parcel, Status.CREATOR);
                break;
            case 20014:
                zzeg.zza(parcel, Status.CREATOR);
                break;
            case 20015:
                zzeg.zza(parcel, Status.CREATOR);
                break;
            case 20016:
                parcel.readInt();
                break;
            case 20017:
                parcel.readInt();
                zzeg.zza(parcel, Uri.CREATOR);
                break;
            case 20018:
                parcel.readInt();
                break;
            case 20019:
                onCaptureOverlayStateChanged(parcel.readInt());
                break;
            case 20020:
                zzd(parcel.readInt(), (Bundle) zzeg.zza(parcel, Bundle.CREATOR));
                break;
            default:
                return false;
        }
        parcel2.writeNoException();
        return true;
    }
}
