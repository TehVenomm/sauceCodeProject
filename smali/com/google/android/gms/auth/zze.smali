.class final Lcom/google/android/gms/auth/zze;
.super Ljava/lang/Object;

# interfaces
.implements Lcom/google/android/gms/auth/zzi;


# annotations
.annotation system Ldalvik/annotation/Signature;
    value = {
        "Ljava/lang/Object;",
        "Lcom/google/android/gms/auth/zzi<",
        "Lcom/google/android/gms/auth/TokenData;",
        ">;"
    }
.end annotation


# instance fields
.field private synthetic val$options:Landroid/os/Bundle;

.field private synthetic zzdxn:Landroid/accounts/Account;

.field private synthetic zzdxo:Ljava/lang/String;


# direct methods
.method constructor <init>(Landroid/accounts/Account;Ljava/lang/String;Landroid/os/Bundle;)V
    .locals 0

    iput-object p1, p0, Lcom/google/android/gms/auth/zze;->zzdxn:Landroid/accounts/Account;

    iput-object p2, p0, Lcom/google/android/gms/auth/zze;->zzdxo:Ljava/lang/String;

    iput-object p3, p0, Lcom/google/android/gms/auth/zze;->val$options:Landroid/os/Bundle;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public final synthetic zzaa(Landroid/os/IBinder;)Ljava/lang/Object;
    .locals 7
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Landroid/os/RemoteException;,
            Ljava/io/IOException;,
            Lcom/google/android/gms/auth/GoogleAuthException;
        }
    .end annotation

    invoke-static {p1}, Lcom/google/android/gms/internal/zzei;->zza(Landroid/os/IBinder;)Lcom/google/android/gms/internal/zzeh;

    move-result-object p1

    iget-object v0, p0, Lcom/google/android/gms/auth/zze;->zzdxn:Landroid/accounts/Account;

    iget-object v1, p0, Lcom/google/android/gms/auth/zze;->zzdxo:Ljava/lang/String;

    iget-object v2, p0, Lcom/google/android/gms/auth/zze;->val$options:Landroid/os/Bundle;

    invoke-interface {p1, v0, v1, v2}, Lcom/google/android/gms/internal/zzeh;->zza(Landroid/accounts/Account;Ljava/lang/String;Landroid/os/Bundle;)Landroid/os/Bundle;

    move-result-object p1

    invoke-static {p1}, Lcom/google/android/gms/auth/zzd;->zzm(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Landroid/os/Bundle;

    const-string v0, "tokenDetails"

    invoke-static {p1, v0}, Lcom/google/android/gms/auth/TokenData;->zzd(Landroid/os/Bundle;Ljava/lang/String;)Lcom/google/android/gms/auth/TokenData;

    move-result-object v0

    if-eqz v0, :cond_0

    return-object v0

    :cond_0
    const-string v0, "Error"

    invoke-virtual {p1, v0}, Landroid/os/Bundle;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    const-string v1, "userRecoveryIntent"

    invoke-virtual {p1, v1}, Landroid/os/Bundle;->getParcelable(Ljava/lang/String;)Landroid/os/Parcelable;

    move-result-object p1

    check-cast p1, Landroid/content/Intent;

    invoke-static {v0}, Lcom/google/android/gms/internal/zzatr;->zzeu(Ljava/lang/String;)Lcom/google/android/gms/internal/zzatr;

    move-result-object v1

    sget-object v2, Lcom/google/android/gms/internal/zzatr;->zzedy:Lcom/google/android/gms/internal/zzatr;

    invoke-virtual {v2, v1}, Lcom/google/android/gms/internal/zzatr;->equals(Ljava/lang/Object;)Z

    move-result v2

    const/4 v3, 0x0

    const/4 v4, 0x1

    if-nez v2, :cond_2

    sget-object v2, Lcom/google/android/gms/internal/zzatr;->zzeeh:Lcom/google/android/gms/internal/zzatr;

    invoke-virtual {v2, v1}, Lcom/google/android/gms/internal/zzatr;->equals(Ljava/lang/Object;)Z

    move-result v2

    if-nez v2, :cond_2

    sget-object v2, Lcom/google/android/gms/internal/zzatr;->zzeek:Lcom/google/android/gms/internal/zzatr;

    invoke-virtual {v2, v1}, Lcom/google/android/gms/internal/zzatr;->equals(Ljava/lang/Object;)Z

    move-result v2

    if-nez v2, :cond_2

    sget-object v2, Lcom/google/android/gms/internal/zzatr;->zzeel:Lcom/google/android/gms/internal/zzatr;

    invoke-virtual {v2, v1}, Lcom/google/android/gms/internal/zzatr;->equals(Ljava/lang/Object;)Z

    move-result v2

    if-nez v2, :cond_2

    sget-object v2, Lcom/google/android/gms/internal/zzatr;->zzeec:Lcom/google/android/gms/internal/zzatr;

    invoke-virtual {v2, v1}, Lcom/google/android/gms/internal/zzatr;->equals(Ljava/lang/Object;)Z

    move-result v2

    if-nez v2, :cond_2

    sget-object v2, Lcom/google/android/gms/internal/zzatr;->zzeen:Lcom/google/android/gms/internal/zzatr;

    invoke-virtual {v2, v1}, Lcom/google/android/gms/internal/zzatr;->equals(Ljava/lang/Object;)Z

    move-result v2

    if-nez v2, :cond_2

    sget-object v2, Lcom/google/android/gms/internal/zzatr;->zzedr:Lcom/google/android/gms/internal/zzatr;

    invoke-virtual {v2, v1}, Lcom/google/android/gms/internal/zzatr;->equals(Ljava/lang/Object;)Z

    move-result v2

    if-nez v2, :cond_2

    sget-object v2, Lcom/google/android/gms/internal/zzatr;->zzees:Lcom/google/android/gms/internal/zzatr;

    invoke-virtual {v2, v1}, Lcom/google/android/gms/internal/zzatr;->equals(Ljava/lang/Object;)Z

    move-result v2

    if-nez v2, :cond_2

    sget-object v2, Lcom/google/android/gms/internal/zzatr;->zzeet:Lcom/google/android/gms/internal/zzatr;

    invoke-virtual {v2, v1}, Lcom/google/android/gms/internal/zzatr;->equals(Ljava/lang/Object;)Z

    move-result v2

    if-nez v2, :cond_2

    sget-object v2, Lcom/google/android/gms/internal/zzatr;->zzeeu:Lcom/google/android/gms/internal/zzatr;

    invoke-virtual {v2, v1}, Lcom/google/android/gms/internal/zzatr;->equals(Ljava/lang/Object;)Z

    move-result v2

    if-nez v2, :cond_2

    sget-object v2, Lcom/google/android/gms/internal/zzatr;->zzeev:Lcom/google/android/gms/internal/zzatr;

    invoke-virtual {v2, v1}, Lcom/google/android/gms/internal/zzatr;->equals(Ljava/lang/Object;)Z

    move-result v2

    if-nez v2, :cond_2

    sget-object v2, Lcom/google/android/gms/internal/zzatr;->zzeew:Lcom/google/android/gms/internal/zzatr;

    invoke-virtual {v2, v1}, Lcom/google/android/gms/internal/zzatr;->equals(Ljava/lang/Object;)Z

    move-result v2

    if-nez v2, :cond_2

    sget-object v2, Lcom/google/android/gms/internal/zzatr;->zzeex:Lcom/google/android/gms/internal/zzatr;

    invoke-virtual {v2, v1}, Lcom/google/android/gms/internal/zzatr;->equals(Ljava/lang/Object;)Z

    move-result v2

    if-nez v2, :cond_2

    sget-object v2, Lcom/google/android/gms/internal/zzatr;->zzeez:Lcom/google/android/gms/internal/zzatr;

    invoke-virtual {v2, v1}, Lcom/google/android/gms/internal/zzatr;->equals(Ljava/lang/Object;)Z

    move-result v2

    if-nez v2, :cond_2

    sget-object v2, Lcom/google/android/gms/internal/zzatr;->zzeer:Lcom/google/android/gms/internal/zzatr;

    invoke-virtual {v2, v1}, Lcom/google/android/gms/internal/zzatr;->equals(Ljava/lang/Object;)Z

    move-result v2

    if-nez v2, :cond_2

    sget-object v2, Lcom/google/android/gms/internal/zzatr;->zzeey:Lcom/google/android/gms/internal/zzatr;

    invoke-virtual {v2, v1}, Lcom/google/android/gms/internal/zzatr;->equals(Ljava/lang/Object;)Z

    move-result v2

    if-eqz v2, :cond_1

    goto :goto_0

    :cond_1
    const/4 v2, 0x0

    goto :goto_1

    :cond_2
    :goto_0
    const/4 v2, 0x1

    :goto_1
    if-nez v2, :cond_6

    sget-object p1, Lcom/google/android/gms/internal/zzatr;->zzedv:Lcom/google/android/gms/internal/zzatr;

    invoke-virtual {p1, v1}, Lcom/google/android/gms/internal/zzatr;->equals(Ljava/lang/Object;)Z

    move-result p1

    if-nez p1, :cond_3

    sget-object p1, Lcom/google/android/gms/internal/zzatr;->zzedw:Lcom/google/android/gms/internal/zzatr;

    invoke-virtual {p1, v1}, Lcom/google/android/gms/internal/zzatr;->equals(Ljava/lang/Object;)Z

    move-result p1

    if-eqz p1, :cond_4

    :cond_3
    const/4 v3, 0x1

    :cond_4
    if-eqz v3, :cond_5

    new-instance p1, Ljava/io/IOException;

    invoke-direct {p1, v0}, Ljava/io/IOException;-><init>(Ljava/lang/String;)V

    throw p1

    :cond_5
    new-instance p1, Lcom/google/android/gms/auth/GoogleAuthException;

    invoke-direct {p1, v0}, Lcom/google/android/gms/auth/GoogleAuthException;-><init>(Ljava/lang/String;)V

    throw p1

    :cond_6
    invoke-static {}, Lcom/google/android/gms/auth/zzd;->zzzt()Lcom/google/android/gms/internal/zzbcq;

    move-result-object v2

    new-array v4, v4, [Ljava/lang/Object;

    invoke-static {v1}, Ljava/lang/String;->valueOf(Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v1

    invoke-static {v1}, Ljava/lang/String;->valueOf(Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v5

    invoke-virtual {v5}, Ljava/lang/String;->length()I

    move-result v5

    add-int/lit8 v5, v5, 0x1f

    new-instance v6, Ljava/lang/StringBuilder;

    invoke-direct {v6, v5}, Ljava/lang/StringBuilder;-><init>(I)V

    const-string v5, "isUserRecoverableError status: "

    invoke-virtual {v6, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v6, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v6}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    aput-object v1, v4, v3

    const-string v1, "GoogleAuthUtil"

    invoke-virtual {v2, v1, v4}, Lcom/google/android/gms/internal/zzbcq;->zzf(Ljava/lang/String;[Ljava/lang/Object;)V

    new-instance v1, Lcom/google/android/gms/auth/UserRecoverableAuthException;

    invoke-direct {v1, v0, p1}, Lcom/google/android/gms/auth/UserRecoverableAuthException;-><init>(Ljava/lang/String;Landroid/content/Intent;)V

    throw v1
.end method
