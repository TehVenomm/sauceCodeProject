.class public final Lcom/google/android/gms/internal/zzbkv;
.super Lcom/google/android/gms/common/internal/safeparcel/zza;

# interfaces
.implements Lcom/google/android/gms/drive/FileUploadPreferences;


# static fields
.field public static final CREATOR:Landroid/os/Parcelable$Creator;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Landroid/os/Parcelable$Creator<",
            "Lcom/google/android/gms/internal/zzbkv;",
            ">;"
        }
    .end annotation
.end field


# instance fields
.field private zzgio:I

.field private zzgip:I

.field private zzgiq:Z


# direct methods
.method static constructor <clinit>()V
    .locals 1

    new-instance v0, Lcom/google/android/gms/internal/zzbkw;

    invoke-direct {v0}, Lcom/google/android/gms/internal/zzbkw;-><init>()V

    sput-object v0, Lcom/google/android/gms/internal/zzbkv;->CREATOR:Landroid/os/Parcelable$Creator;

    return-void
.end method

.method public constructor <init>(IIZ)V
    .locals 0

    invoke-direct {p0}, Lcom/google/android/gms/common/internal/safeparcel/zza;-><init>()V

    iput p1, p0, Lcom/google/android/gms/internal/zzbkv;->zzgio:I

    iput p2, p0, Lcom/google/android/gms/internal/zzbkv;->zzgip:I

    iput-boolean p3, p0, Lcom/google/android/gms/internal/zzbkv;->zzgiq:Z

    return-void
.end method

.method private static zzcs(I)Z
    .locals 0

    packed-switch p0, :pswitch_data_0

    const/4 p0, 0x0

    return p0

    :pswitch_0
    const/4 p0, 0x1

    return p0

    nop

    :pswitch_data_0
    .packed-switch 0x1
        :pswitch_0
        :pswitch_0
    .end packed-switch
.end method

.method private static zzct(I)Z
    .locals 0

    packed-switch p0, :pswitch_data_0

    const/4 p0, 0x0

    return p0

    :pswitch_0
    const/4 p0, 0x1

    return p0

    nop

    :pswitch_data_0
    .packed-switch 0x100
        :pswitch_0
        :pswitch_0
    .end packed-switch
.end method


# virtual methods
.method public final getBatteryUsagePreference()I
    .locals 1

    iget v0, p0, Lcom/google/android/gms/internal/zzbkv;->zzgip:I

    invoke-static {v0}, Lcom/google/android/gms/internal/zzbkv;->zzct(I)Z

    move-result v0

    if-nez v0, :cond_0

    const/4 v0, 0x0

    return v0

    :cond_0
    iget v0, p0, Lcom/google/android/gms/internal/zzbkv;->zzgip:I

    return v0
.end method

.method public final getNetworkTypePreference()I
    .locals 1

    iget v0, p0, Lcom/google/android/gms/internal/zzbkv;->zzgio:I

    invoke-static {v0}, Lcom/google/android/gms/internal/zzbkv;->zzcs(I)Z

    move-result v0

    if-nez v0, :cond_0

    const/4 v0, 0x0

    return v0

    :cond_0
    iget v0, p0, Lcom/google/android/gms/internal/zzbkv;->zzgio:I

    return v0
.end method

.method public final isRoamingAllowed()Z
    .locals 1

    iget-boolean v0, p0, Lcom/google/android/gms/internal/zzbkv;->zzgiq:Z

    return v0
.end method

.method public final setBatteryUsagePreference(I)V
    .locals 1

    invoke-static {p1}, Lcom/google/android/gms/internal/zzbkv;->zzct(I)Z

    move-result v0

    if-eqz v0, :cond_0

    iput p1, p0, Lcom/google/android/gms/internal/zzbkv;->zzgip:I

    return-void

    :cond_0
    new-instance p1, Ljava/lang/IllegalArgumentException;

    const-string v0, "Invalid battery usage preference value."

    invoke-direct {p1, v0}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw p1
.end method

.method public final setNetworkTypePreference(I)V
    .locals 1

    invoke-static {p1}, Lcom/google/android/gms/internal/zzbkv;->zzcs(I)Z

    move-result v0

    if-eqz v0, :cond_0

    iput p1, p0, Lcom/google/android/gms/internal/zzbkv;->zzgio:I

    return-void

    :cond_0
    new-instance p1, Ljava/lang/IllegalArgumentException;

    const-string v0, "Invalid data connection preference value."

    invoke-direct {p1, v0}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw p1
.end method

.method public final setRoamingAllowed(Z)V
    .locals 0

    iput-boolean p1, p0, Lcom/google/android/gms/internal/zzbkv;->zzgiq:Z

    return-void
.end method

.method public final writeToParcel(Landroid/os/Parcel;I)V
    .locals 2

    invoke-static {p1}, Lcom/google/android/gms/common/internal/safeparcel/zzd;->zze(Landroid/os/Parcel;)I

    move-result p2

    iget v0, p0, Lcom/google/android/gms/internal/zzbkv;->zzgio:I

    const/4 v1, 0x2

    invoke-static {p1, v1, v0}, Lcom/google/android/gms/common/internal/safeparcel/zzd;->zzc(Landroid/os/Parcel;II)V

    iget v0, p0, Lcom/google/android/gms/internal/zzbkv;->zzgip:I

    const/4 v1, 0x3

    invoke-static {p1, v1, v0}, Lcom/google/android/gms/common/internal/safeparcel/zzd;->zzc(Landroid/os/Parcel;II)V

    iget-boolean v0, p0, Lcom/google/android/gms/internal/zzbkv;->zzgiq:Z

    const/4 v1, 0x4

    invoke-static {p1, v1, v0}, Lcom/google/android/gms/common/internal/safeparcel/zzd;->zza(Landroid/os/Parcel;IZ)V

    invoke-static {p1, p2}, Lcom/google/android/gms/common/internal/safeparcel/zzd;->zzai(Landroid/os/Parcel;I)V

    return-void
.end method
