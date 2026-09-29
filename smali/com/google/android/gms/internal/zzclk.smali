.class public final Lcom/google/android/gms/internal/zzclk;
.super Lcom/google/android/gms/common/internal/safeparcel/zza;


# static fields
.field public static final CREATOR:Landroid/os/Parcelable$Creator;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Landroid/os/Parcelable$Creator<",
            "Lcom/google/android/gms/internal/zzclk;",
            ">;"
        }
    .end annotation
.end field


# instance fields
.field private final zzdxt:I

.field private final zzjey:Landroid/os/ParcelUuid;

.field private final zzjez:Landroid/os/ParcelUuid;

.field private final zzjfa:Landroid/os/ParcelUuid;

.field private final zzjfb:[B

.field private final zzjfc:[B

.field private final zzjfd:I

.field private final zzjfe:[B

.field private final zzjff:[B


# direct methods
.method static constructor <clinit>()V
    .locals 1

    new-instance v0, Lcom/google/android/gms/internal/zzcll;

    invoke-direct {v0}, Lcom/google/android/gms/internal/zzcll;-><init>()V

    sput-object v0, Lcom/google/android/gms/internal/zzclk;->CREATOR:Landroid/os/Parcelable$Creator;

    return-void
.end method

.method constructor <init>(ILandroid/os/ParcelUuid;Landroid/os/ParcelUuid;Landroid/os/ParcelUuid;[B[BI[B[B)V
    .locals 0

    invoke-direct {p0}, Lcom/google/android/gms/common/internal/safeparcel/zza;-><init>()V

    iput p1, p0, Lcom/google/android/gms/internal/zzclk;->zzdxt:I

    iput-object p2, p0, Lcom/google/android/gms/internal/zzclk;->zzjey:Landroid/os/ParcelUuid;

    iput-object p3, p0, Lcom/google/android/gms/internal/zzclk;->zzjez:Landroid/os/ParcelUuid;

    iput-object p4, p0, Lcom/google/android/gms/internal/zzclk;->zzjfa:Landroid/os/ParcelUuid;

    iput-object p5, p0, Lcom/google/android/gms/internal/zzclk;->zzjfb:[B

    iput-object p6, p0, Lcom/google/android/gms/internal/zzclk;->zzjfc:[B

    iput p7, p0, Lcom/google/android/gms/internal/zzclk;->zzjfd:I

    iput-object p8, p0, Lcom/google/android/gms/internal/zzclk;->zzjfe:[B

    iput-object p9, p0, Lcom/google/android/gms/internal/zzclk;->zzjff:[B

    return-void
.end method


# virtual methods
.method public final equals(Ljava/lang/Object;)Z
    .locals 4

    const/4 v0, 0x1

    if-ne p0, p1, :cond_0

    return v0

    :cond_0
    const/4 v1, 0x0

    if-eqz p1, :cond_2

    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v2

    invoke-virtual {p1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v3

    if-eq v2, v3, :cond_1

    goto :goto_0

    :cond_1
    check-cast p1, Lcom/google/android/gms/internal/zzclk;

    iget v2, p0, Lcom/google/android/gms/internal/zzclk;->zzjfd:I

    iget v3, p1, Lcom/google/android/gms/internal/zzclk;->zzjfd:I

    if-ne v2, v3, :cond_2

    iget-object v2, p0, Lcom/google/android/gms/internal/zzclk;->zzjfe:[B

    iget-object v3, p1, Lcom/google/android/gms/internal/zzclk;->zzjfe:[B

    invoke-static {v2, v3}, Ljava/util/Arrays;->equals([B[B)Z

    move-result v2

    if-eqz v2, :cond_2

    iget-object v2, p0, Lcom/google/android/gms/internal/zzclk;->zzjff:[B

    iget-object v3, p1, Lcom/google/android/gms/internal/zzclk;->zzjff:[B

    invoke-static {v2, v3}, Ljava/util/Arrays;->equals([B[B)Z

    move-result v2

    if-eqz v2, :cond_2

    iget-object v2, p0, Lcom/google/android/gms/internal/zzclk;->zzjfa:Landroid/os/ParcelUuid;

    iget-object v3, p1, Lcom/google/android/gms/internal/zzclk;->zzjfa:Landroid/os/ParcelUuid;

    invoke-static {v2, v3}, Lcom/google/android/gms/common/internal/zzbf;->equal(Ljava/lang/Object;Ljava/lang/Object;)Z

    move-result v2

    if-eqz v2, :cond_2

    iget-object v2, p0, Lcom/google/android/gms/internal/zzclk;->zzjfb:[B

    iget-object v3, p1, Lcom/google/android/gms/internal/zzclk;->zzjfb:[B

    invoke-static {v2, v3}, Ljava/util/Arrays;->equals([B[B)Z

    move-result v2

    if-eqz v2, :cond_2

    iget-object v2, p0, Lcom/google/android/gms/internal/zzclk;->zzjfc:[B

    iget-object v3, p1, Lcom/google/android/gms/internal/zzclk;->zzjfc:[B

    invoke-static {v2, v3}, Ljava/util/Arrays;->equals([B[B)Z

    move-result v2

    if-eqz v2, :cond_2

    iget-object v2, p0, Lcom/google/android/gms/internal/zzclk;->zzjey:Landroid/os/ParcelUuid;

    iget-object v3, p1, Lcom/google/android/gms/internal/zzclk;->zzjey:Landroid/os/ParcelUuid;

    invoke-static {v2, v3}, Lcom/google/android/gms/common/internal/zzbf;->equal(Ljava/lang/Object;Ljava/lang/Object;)Z

    move-result v2

    if-eqz v2, :cond_2

    iget-object v2, p0, Lcom/google/android/gms/internal/zzclk;->zzjez:Landroid/os/ParcelUuid;

    iget-object p1, p1, Lcom/google/android/gms/internal/zzclk;->zzjez:Landroid/os/ParcelUuid;

    invoke-static {v2, p1}, Lcom/google/android/gms/common/internal/zzbf;->equal(Ljava/lang/Object;Ljava/lang/Object;)Z

    move-result p1

    if-eqz p1, :cond_2

    return v0

    :cond_2
    :goto_0
    return v1
.end method

.method public final hashCode()I
    .locals 3

    const/16 v0, 0x8

    new-array v0, v0, [Ljava/lang/Object;

    iget v1, p0, Lcom/google/android/gms/internal/zzclk;->zzjfd:I

    invoke-static {v1}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v1

    const/4 v2, 0x0

    aput-object v1, v0, v2

    iget-object v1, p0, Lcom/google/android/gms/internal/zzclk;->zzjfe:[B

    invoke-static {v1}, Ljava/util/Arrays;->hashCode([B)I

    move-result v1

    invoke-static {v1}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v1

    const/4 v2, 0x1

    aput-object v1, v0, v2

    iget-object v1, p0, Lcom/google/android/gms/internal/zzclk;->zzjff:[B

    invoke-static {v1}, Ljava/util/Arrays;->hashCode([B)I

    move-result v1

    invoke-static {v1}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v1

    const/4 v2, 0x2

    aput-object v1, v0, v2

    iget-object v1, p0, Lcom/google/android/gms/internal/zzclk;->zzjfa:Landroid/os/ParcelUuid;

    const/4 v2, 0x3

    aput-object v1, v0, v2

    iget-object v1, p0, Lcom/google/android/gms/internal/zzclk;->zzjfb:[B

    invoke-static {v1}, Ljava/util/Arrays;->hashCode([B)I

    move-result v1

    invoke-static {v1}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v1

    const/4 v2, 0x4

    aput-object v1, v0, v2

    iget-object v1, p0, Lcom/google/android/gms/internal/zzclk;->zzjfc:[B

    invoke-static {v1}, Ljava/util/Arrays;->hashCode([B)I

    move-result v1

    invoke-static {v1}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v1

    const/4 v2, 0x5

    aput-object v1, v0, v2

    iget-object v1, p0, Lcom/google/android/gms/internal/zzclk;->zzjey:Landroid/os/ParcelUuid;

    const/4 v2, 0x6

    aput-object v1, v0, v2

    iget-object v1, p0, Lcom/google/android/gms/internal/zzclk;->zzjez:Landroid/os/ParcelUuid;

    const/4 v2, 0x7

    aput-object v1, v0, v2

    invoke-static {v0}, Ljava/util/Arrays;->hashCode([Ljava/lang/Object;)I

    move-result v0

    return v0
.end method

.method public final writeToParcel(Landroid/os/Parcel;I)V
    .locals 4

    invoke-static {p1}, Lcom/google/android/gms/common/internal/safeparcel/zzd;->zze(Landroid/os/Parcel;)I

    move-result v0

    iget v1, p0, Lcom/google/android/gms/internal/zzclk;->zzdxt:I

    const/4 v2, 0x1

    invoke-static {p1, v2, v1}, Lcom/google/android/gms/common/internal/safeparcel/zzd;->zzc(Landroid/os/Parcel;II)V

    iget-object v1, p0, Lcom/google/android/gms/internal/zzclk;->zzjey:Landroid/os/ParcelUuid;

    const/4 v2, 0x0

    const/4 v3, 0x4

    invoke-static {p1, v3, v1, p2, v2}, Lcom/google/android/gms/common/internal/safeparcel/zzd;->zza(Landroid/os/Parcel;ILandroid/os/Parcelable;IZ)V

    iget-object v1, p0, Lcom/google/android/gms/internal/zzclk;->zzjez:Landroid/os/ParcelUuid;

    const/4 v3, 0x5

    invoke-static {p1, v3, v1, p2, v2}, Lcom/google/android/gms/common/internal/safeparcel/zzd;->zza(Landroid/os/Parcel;ILandroid/os/Parcelable;IZ)V

    iget-object v1, p0, Lcom/google/android/gms/internal/zzclk;->zzjfa:Landroid/os/ParcelUuid;

    const/4 v3, 0x6

    invoke-static {p1, v3, v1, p2, v2}, Lcom/google/android/gms/common/internal/safeparcel/zzd;->zza(Landroid/os/Parcel;ILandroid/os/Parcelable;IZ)V

    iget-object p2, p0, Lcom/google/android/gms/internal/zzclk;->zzjfb:[B

    const/4 v1, 0x7

    invoke-static {p1, v1, p2, v2}, Lcom/google/android/gms/common/internal/safeparcel/zzd;->zza(Landroid/os/Parcel;I[BZ)V

    iget-object p2, p0, Lcom/google/android/gms/internal/zzclk;->zzjfc:[B

    const/16 v1, 0x8

    invoke-static {p1, v1, p2, v2}, Lcom/google/android/gms/common/internal/safeparcel/zzd;->zza(Landroid/os/Parcel;I[BZ)V

    iget p2, p0, Lcom/google/android/gms/internal/zzclk;->zzjfd:I

    const/16 v1, 0x9

    invoke-static {p1, v1, p2}, Lcom/google/android/gms/common/internal/safeparcel/zzd;->zzc(Landroid/os/Parcel;II)V

    iget-object p2, p0, Lcom/google/android/gms/internal/zzclk;->zzjfe:[B

    const/16 v1, 0xa

    invoke-static {p1, v1, p2, v2}, Lcom/google/android/gms/common/internal/safeparcel/zzd;->zza(Landroid/os/Parcel;I[BZ)V

    iget-object p2, p0, Lcom/google/android/gms/internal/zzclk;->zzjff:[B

    const/16 v1, 0xb

    invoke-static {p1, v1, p2, v2}, Lcom/google/android/gms/common/internal/safeparcel/zzd;->zza(Landroid/os/Parcel;I[BZ)V

    invoke-static {p1, v0}, Lcom/google/android/gms/common/internal/safeparcel/zzd;->zzai(Landroid/os/Parcel;I)V

    return-void
.end method
