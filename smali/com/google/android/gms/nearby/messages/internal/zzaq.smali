.class final Lcom/google/android/gms/nearby/messages/internal/zzaq;
.super Lcom/google/android/gms/nearby/messages/internal/zzav;


# instance fields
.field private synthetic zzgwu:Landroid/app/PendingIntent;

.field private synthetic zzjgg:Lcom/google/android/gms/common/api/internal/zzcj;

.field private synthetic zzjgi:Lcom/google/android/gms/nearby/messages/SubscribeOptions;


# direct methods
.method constructor <init>(Lcom/google/android/gms/nearby/messages/internal/zzak;Lcom/google/android/gms/common/api/GoogleApiClient;Landroid/app/PendingIntent;Lcom/google/android/gms/common/api/internal/zzcj;Lcom/google/android/gms/nearby/messages/SubscribeOptions;)V
    .locals 0

    iput-object p3, p0, Lcom/google/android/gms/nearby/messages/internal/zzaq;->zzgwu:Landroid/app/PendingIntent;

    iput-object p4, p0, Lcom/google/android/gms/nearby/messages/internal/zzaq;->zzjgg:Lcom/google/android/gms/common/api/internal/zzcj;

    iput-object p5, p0, Lcom/google/android/gms/nearby/messages/internal/zzaq;->zzjgi:Lcom/google/android/gms/nearby/messages/SubscribeOptions;

    invoke-direct {p0, p2}, Lcom/google/android/gms/nearby/messages/internal/zzav;-><init>(Lcom/google/android/gms/common/api/GoogleApiClient;)V

    return-void
.end method


# virtual methods
.method protected final synthetic zza(Lcom/google/android/gms/common/api/Api$zzb;)V
    .locals 12
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Landroid/os/RemoteException;
        }
    .end annotation

    check-cast p1, Lcom/google/android/gms/nearby/messages/internal/zzah;

    invoke-virtual {p0}, Lcom/google/android/gms/nearby/messages/internal/zzav;->zzbbb()Lcom/google/android/gms/common/api/internal/zzcj;

    move-result-object v0

    iget-object v6, p0, Lcom/google/android/gms/nearby/messages/internal/zzaq;->zzgwu:Landroid/app/PendingIntent;

    iget-object v1, p0, Lcom/google/android/gms/nearby/messages/internal/zzaq;->zzjgg:Lcom/google/android/gms/common/api/internal/zzcj;

    iget-object v2, p0, Lcom/google/android/gms/nearby/messages/internal/zzaq;->zzjgi:Lcom/google/android/gms/nearby/messages/SubscribeOptions;

    new-instance v11, Lcom/google/android/gms/nearby/messages/internal/SubscribeRequest;

    invoke-virtual {v2}, Lcom/google/android/gms/nearby/messages/SubscribeOptions;->getStrategy()Lcom/google/android/gms/nearby/messages/Strategy;

    move-result-object v3

    new-instance v4, Lcom/google/android/gms/internal/zzclt;

    invoke-direct {v4, v0}, Lcom/google/android/gms/internal/zzclt;-><init>(Lcom/google/android/gms/common/api/internal/zzcj;)V

    invoke-virtual {v2}, Lcom/google/android/gms/nearby/messages/SubscribeOptions;->getFilter()Lcom/google/android/gms/nearby/messages/MessageFilter;

    move-result-object v5

    if-nez v1, :cond_0

    const/4 v0, 0x0

    :goto_0
    move-object v8, v0

    goto :goto_1

    :cond_0
    new-instance v0, Lcom/google/android/gms/internal/zzcma;

    invoke-direct {v0, v1}, Lcom/google/android/gms/internal/zzcma;-><init>(Lcom/google/android/gms/common/api/internal/zzcj;)V

    goto :goto_0

    :goto_1
    iget-boolean v9, v2, Lcom/google/android/gms/nearby/messages/SubscribeOptions;->zzjev:Z

    iget v10, v2, Lcom/google/android/gms/nearby/messages/SubscribeOptions;->zzjew:I

    const/4 v2, 0x0

    const/4 v7, 0x0

    move-object v1, v11

    invoke-direct/range {v1 .. v10}, Lcom/google/android/gms/nearby/messages/internal/SubscribeRequest;-><init>(Landroid/os/IBinder;Lcom/google/android/gms/nearby/messages/Strategy;Landroid/os/IBinder;Lcom/google/android/gms/nearby/messages/MessageFilter;Landroid/app/PendingIntent;[BLandroid/os/IBinder;ZI)V

    invoke-virtual {p1}, Lcom/google/android/gms/common/internal/zzd;->zzajj()Landroid/os/IInterface;

    move-result-object p1

    check-cast p1, Lcom/google/android/gms/nearby/messages/internal/zzs;

    invoke-interface {p1, v11}, Lcom/google/android/gms/nearby/messages/internal/zzs;->zza(Lcom/google/android/gms/nearby/messages/internal/SubscribeRequest;)V

    return-void
.end method
