.class public final Lcom/google/android/gms/internal/zzbmv;
.super Lcom/google/android/gms/common/internal/safeparcel/zza;


# static fields
.field public static final CREATOR:Landroid/os/Parcelable$Creator;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Landroid/os/Parcelable$Creator<",
            "Lcom/google/android/gms/internal/zzbmv;",
            ">;"
        }
    .end annotation
.end field


# instance fields
.field private zzgio:I

.field private zzgip:I

.field private zzgju:Z


# direct methods
.method static constructor <clinit>()V
    .locals 1

    new-instance v0, Lcom/google/android/gms/internal/zzbmw;

    invoke-direct {v0}, Lcom/google/android/gms/internal/zzbmw;-><init>()V

    sput-object v0, Lcom/google/android/gms/internal/zzbmv;->CREATOR:Landroid/os/Parcelable$Creator;

    return-void
.end method

.method constructor <init>(IIZ)V
    .locals 0

    invoke-direct {p0}, Lcom/google/android/gms/common/internal/safeparcel/zza;-><init>()V

    iput p1, p0, Lcom/google/android/gms/internal/zzbmv;->zzgio:I

    iput p2, p0, Lcom/google/android/gms/internal/zzbmv;->zzgip:I

    iput-boolean p3, p0, Lcom/google/android/gms/internal/zzbmv;->zzgju:Z

    return-void
.end method


# virtual methods
.method public final writeToParcel(Landroid/os/Parcel;I)V
    .locals 2

    invoke-static {p1}, Lcom/google/android/gms/common/internal/safeparcel/zzd;->zze(Landroid/os/Parcel;)I

    move-result p2

    iget v0, p0, Lcom/google/android/gms/internal/zzbmv;->zzgio:I

    const/4 v1, 0x2

    invoke-static {p1, v1, v0}, Lcom/google/android/gms/common/internal/safeparcel/zzd;->zzc(Landroid/os/Parcel;II)V

    iget v0, p0, Lcom/google/android/gms/internal/zzbmv;->zzgip:I

    const/4 v1, 0x3

    invoke-static {p1, v1, v0}, Lcom/google/android/gms/common/internal/safeparcel/zzd;->zzc(Landroid/os/Parcel;II)V

    iget-boolean v0, p0, Lcom/google/android/gms/internal/zzbmv;->zzgju:Z

    const/4 v1, 0x4

    invoke-static {p1, v1, v0}, Lcom/google/android/gms/common/internal/safeparcel/zzd;->zza(Landroid/os/Parcel;IZ)V

    invoke-static {p1, p2}, Lcom/google/android/gms/common/internal/safeparcel/zzd;->zzai(Landroid/os/Parcel;I)V

    return-void
.end method
