.class public final Lcom/google/android/gms/internal/zzcgl;
.super Lcom/google/android/gms/common/internal/safeparcel/zza;


# static fields
.field public static final CREATOR:Landroid/os/Parcelable$Creator;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Landroid/os/Parcelable$Creator<",
            "Lcom/google/android/gms/internal/zzcgl;",
            ">;"
        }
    .end annotation
.end field


# instance fields
.field private final zzjaz:Lcom/google/android/gms/internal/zzcjl;
    .annotation build Landroidx/annotation/Nullable;
    .end annotation
.end field

.field private final zzjba:Lcom/google/android/gms/internal/zzcis;
    .annotation build Landroidx/annotation/Nullable;
    .end annotation
.end field

.field private final zzjbb:Ljava/lang/String;

.field private final zzjbc:[B
    .annotation build Landroidx/annotation/Nullable;
    .end annotation
.end field

.field private final zzjbd:Lcom/google/android/gms/internal/zzcji;
    .annotation build Landroidx/annotation/Nullable;
    .end annotation
.end field


# direct methods
.method static constructor <clinit>()V
    .locals 1

    new-instance v0, Lcom/google/android/gms/internal/zzcgm;

    invoke-direct {v0}, Lcom/google/android/gms/internal/zzcgm;-><init>()V

    sput-object v0, Lcom/google/android/gms/internal/zzcgl;->CREATOR:Landroid/os/Parcelable$Creator;

    return-void
.end method

.method public constructor <init>(Landroid/os/IBinder;Landroid/os/IBinder;Ljava/lang/String;[BLandroid/os/IBinder;)V
    .locals 8
    .param p1    # Landroid/os/IBinder;
        .annotation build Landroidx/annotation/Nullable;
        .end annotation
    .end param
    .param p2    # Landroid/os/IBinder;
        .annotation build Landroidx/annotation/Nullable;
        .end annotation
    .end param
    .param p4    # [B
        .annotation build Landroidx/annotation/Nullable;
        .end annotation
    .end param
    .param p5    # Landroid/os/IBinder;
        .annotation build Landroidx/annotation/Nullable;
        .end annotation
    .end param

    const/4 v0, 0x0

    if-nez p1, :cond_0

    move-object v3, v0

    goto :goto_0

    :cond_0
    const-string v1, "com.google.android.gms.nearby.internal.connection.IResultListener"

    invoke-interface {p1, v1}, Landroid/os/IBinder;->queryLocalInterface(Ljava/lang/String;)Landroid/os/IInterface;

    move-result-object v1

    instance-of v2, v1, Lcom/google/android/gms/internal/zzcjl;

    if-eqz v2, :cond_1

    move-object p1, v1

    check-cast p1, Lcom/google/android/gms/internal/zzcjl;

    move-object v3, p1

    goto :goto_0

    :cond_1
    new-instance v1, Lcom/google/android/gms/internal/zzcjn;

    invoke-direct {v1, p1}, Lcom/google/android/gms/internal/zzcjn;-><init>(Landroid/os/IBinder;)V

    move-object v3, v1

    :goto_0
    if-nez p2, :cond_2

    move-object v4, v0

    goto :goto_2

    :cond_2
    const-string p1, "com.google.android.gms.nearby.internal.connection.IConnectionEventListener"

    invoke-interface {p2, p1}, Landroid/os/IBinder;->queryLocalInterface(Ljava/lang/String;)Landroid/os/IInterface;

    move-result-object p1

    instance-of v1, p1, Lcom/google/android/gms/internal/zzcis;

    if-eqz v1, :cond_3

    check-cast p1, Lcom/google/android/gms/internal/zzcis;

    :goto_1
    move-object v4, p1

    goto :goto_2

    :cond_3
    new-instance p1, Lcom/google/android/gms/internal/zzciu;

    invoke-direct {p1, p2}, Lcom/google/android/gms/internal/zzciu;-><init>(Landroid/os/IBinder;)V

    goto :goto_1

    :goto_2
    if-nez p5, :cond_4

    :goto_3
    move-object v7, v0

    goto :goto_4

    :cond_4
    const-string p1, "com.google.android.gms.nearby.internal.connection.IPayloadListener"

    invoke-interface {p5, p1}, Landroid/os/IBinder;->queryLocalInterface(Ljava/lang/String;)Landroid/os/IInterface;

    move-result-object p1

    instance-of p2, p1, Lcom/google/android/gms/internal/zzcji;

    if-eqz p2, :cond_5

    move-object v0, p1

    check-cast v0, Lcom/google/android/gms/internal/zzcji;

    goto :goto_3

    :cond_5
    new-instance v0, Lcom/google/android/gms/internal/zzcjk;

    invoke-direct {v0, p5}, Lcom/google/android/gms/internal/zzcjk;-><init>(Landroid/os/IBinder;)V

    goto :goto_3

    :goto_4
    move-object v2, p0

    move-object v5, p3

    move-object v6, p4

    invoke-direct/range {v2 .. v7}, Lcom/google/android/gms/internal/zzcgl;-><init>(Lcom/google/android/gms/internal/zzcjl;Lcom/google/android/gms/internal/zzcis;Ljava/lang/String;[BLcom/google/android/gms/internal/zzcji;)V

    return-void
.end method

.method private constructor <init>(Lcom/google/android/gms/internal/zzcjl;Lcom/google/android/gms/internal/zzcis;Ljava/lang/String;[BLcom/google/android/gms/internal/zzcji;)V
    .locals 0
    .param p1    # Lcom/google/android/gms/internal/zzcjl;
        .annotation build Landroidx/annotation/Nullable;
        .end annotation
    .end param
    .param p2    # Lcom/google/android/gms/internal/zzcis;
        .annotation build Landroidx/annotation/Nullable;
        .end annotation
    .end param
    .param p4    # [B
        .annotation build Landroidx/annotation/Nullable;
        .end annotation
    .end param
    .param p5    # Lcom/google/android/gms/internal/zzcji;
        .annotation build Landroidx/annotation/Nullable;
        .end annotation
    .end param

    invoke-direct {p0}, Lcom/google/android/gms/common/internal/safeparcel/zza;-><init>()V

    iput-object p1, p0, Lcom/google/android/gms/internal/zzcgl;->zzjaz:Lcom/google/android/gms/internal/zzcjl;

    iput-object p2, p0, Lcom/google/android/gms/internal/zzcgl;->zzjba:Lcom/google/android/gms/internal/zzcis;

    iput-object p3, p0, Lcom/google/android/gms/internal/zzcgl;->zzjbb:Ljava/lang/String;

    iput-object p4, p0, Lcom/google/android/gms/internal/zzcgl;->zzjbc:[B

    iput-object p5, p0, Lcom/google/android/gms/internal/zzcgl;->zzjbd:Lcom/google/android/gms/internal/zzcji;

    return-void
.end method


# virtual methods
.method public final equals(Ljava/lang/Object;)Z
    .locals 4

    const/4 v0, 0x1

    if-ne p0, p1, :cond_0

    return v0

    :cond_0
    instance-of v1, p1, Lcom/google/android/gms/internal/zzcgl;

    const/4 v2, 0x0

    if-eqz v1, :cond_1

    check-cast p1, Lcom/google/android/gms/internal/zzcgl;

    iget-object v1, p0, Lcom/google/android/gms/internal/zzcgl;->zzjaz:Lcom/google/android/gms/internal/zzcjl;

    iget-object v3, p1, Lcom/google/android/gms/internal/zzcgl;->zzjaz:Lcom/google/android/gms/internal/zzcjl;

    invoke-static {v1, v3}, Lcom/google/android/gms/common/internal/zzbf;->equal(Ljava/lang/Object;Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_1

    iget-object v1, p0, Lcom/google/android/gms/internal/zzcgl;->zzjba:Lcom/google/android/gms/internal/zzcis;

    iget-object v3, p1, Lcom/google/android/gms/internal/zzcgl;->zzjba:Lcom/google/android/gms/internal/zzcis;

    invoke-static {v1, v3}, Lcom/google/android/gms/common/internal/zzbf;->equal(Ljava/lang/Object;Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_1

    iget-object v1, p0, Lcom/google/android/gms/internal/zzcgl;->zzjbb:Ljava/lang/String;

    iget-object v3, p1, Lcom/google/android/gms/internal/zzcgl;->zzjbb:Ljava/lang/String;

    invoke-static {v1, v3}, Lcom/google/android/gms/common/internal/zzbf;->equal(Ljava/lang/Object;Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_1

    iget-object v1, p0, Lcom/google/android/gms/internal/zzcgl;->zzjbc:[B

    iget-object v3, p1, Lcom/google/android/gms/internal/zzcgl;->zzjbc:[B

    invoke-static {v1, v3}, Lcom/google/android/gms/common/internal/zzbf;->equal(Ljava/lang/Object;Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_1

    iget-object v1, p0, Lcom/google/android/gms/internal/zzcgl;->zzjbd:Lcom/google/android/gms/internal/zzcji;

    iget-object p1, p1, Lcom/google/android/gms/internal/zzcgl;->zzjbd:Lcom/google/android/gms/internal/zzcji;

    invoke-static {v1, p1}, Lcom/google/android/gms/common/internal/zzbf;->equal(Ljava/lang/Object;Ljava/lang/Object;)Z

    move-result p1

    if-eqz p1, :cond_1

    return v0

    :cond_1
    return v2
.end method

.method public final hashCode()I
    .locals 3

    const/4 v0, 0x5

    new-array v0, v0, [Ljava/lang/Object;

    iget-object v1, p0, Lcom/google/android/gms/internal/zzcgl;->zzjaz:Lcom/google/android/gms/internal/zzcjl;

    const/4 v2, 0x0

    aput-object v1, v0, v2

    iget-object v1, p0, Lcom/google/android/gms/internal/zzcgl;->zzjba:Lcom/google/android/gms/internal/zzcis;

    const/4 v2, 0x1

    aput-object v1, v0, v2

    iget-object v1, p0, Lcom/google/android/gms/internal/zzcgl;->zzjbb:Ljava/lang/String;

    const/4 v2, 0x2

    aput-object v1, v0, v2

    iget-object v1, p0, Lcom/google/android/gms/internal/zzcgl;->zzjbc:[B

    const/4 v2, 0x3

    aput-object v1, v0, v2

    iget-object v1, p0, Lcom/google/android/gms/internal/zzcgl;->zzjbd:Lcom/google/android/gms/internal/zzcji;

    const/4 v2, 0x4

    aput-object v1, v0, v2

    invoke-static {v0}, Ljava/util/Arrays;->hashCode([Ljava/lang/Object;)I

    move-result v0

    return v0
.end method

.method public final writeToParcel(Landroid/os/Parcel;I)V
    .locals 4

    invoke-static {p1}, Lcom/google/android/gms/common/internal/safeparcel/zzd;->zze(Landroid/os/Parcel;)I

    move-result p2

    iget-object v0, p0, Lcom/google/android/gms/internal/zzcgl;->zzjaz:Lcom/google/android/gms/internal/zzcjl;

    const/4 v1, 0x0

    if-nez v0, :cond_0

    move-object v0, v1

    goto :goto_0

    :cond_0
    iget-object v0, p0, Lcom/google/android/gms/internal/zzcgl;->zzjaz:Lcom/google/android/gms/internal/zzcjl;

    invoke-interface {v0}, Lcom/google/android/gms/internal/zzcjl;->asBinder()Landroid/os/IBinder;

    move-result-object v0

    :goto_0
    const/4 v2, 0x0

    const/4 v3, 0x1

    invoke-static {p1, v3, v0, v2}, Lcom/google/android/gms/common/internal/safeparcel/zzd;->zza(Landroid/os/Parcel;ILandroid/os/IBinder;Z)V

    const/4 v0, 0x2

    iget-object v3, p0, Lcom/google/android/gms/internal/zzcgl;->zzjba:Lcom/google/android/gms/internal/zzcis;

    if-nez v3, :cond_1

    move-object v3, v1

    goto :goto_1

    :cond_1
    iget-object v3, p0, Lcom/google/android/gms/internal/zzcgl;->zzjba:Lcom/google/android/gms/internal/zzcis;

    invoke-interface {v3}, Lcom/google/android/gms/internal/zzcis;->asBinder()Landroid/os/IBinder;

    move-result-object v3

    :goto_1
    invoke-static {p1, v0, v3, v2}, Lcom/google/android/gms/common/internal/safeparcel/zzd;->zza(Landroid/os/Parcel;ILandroid/os/IBinder;Z)V

    const/4 v0, 0x3

    iget-object v3, p0, Lcom/google/android/gms/internal/zzcgl;->zzjbb:Ljava/lang/String;

    invoke-static {p1, v0, v3, v2}, Lcom/google/android/gms/common/internal/safeparcel/zzd;->zza(Landroid/os/Parcel;ILjava/lang/String;Z)V

    const/4 v0, 0x4

    iget-object v3, p0, Lcom/google/android/gms/internal/zzcgl;->zzjbc:[B

    invoke-static {p1, v0, v3, v2}, Lcom/google/android/gms/common/internal/safeparcel/zzd;->zza(Landroid/os/Parcel;I[BZ)V

    const/4 v0, 0x5

    iget-object v3, p0, Lcom/google/android/gms/internal/zzcgl;->zzjbd:Lcom/google/android/gms/internal/zzcji;

    if-nez v3, :cond_2

    goto :goto_2

    :cond_2
    iget-object v1, p0, Lcom/google/android/gms/internal/zzcgl;->zzjbd:Lcom/google/android/gms/internal/zzcji;

    invoke-interface {v1}, Lcom/google/android/gms/internal/zzcji;->asBinder()Landroid/os/IBinder;

    move-result-object v1

    :goto_2
    invoke-static {p1, v0, v1, v2}, Lcom/google/android/gms/common/internal/safeparcel/zzd;->zza(Landroid/os/Parcel;ILandroid/os/IBinder;Z)V

    invoke-static {p1, p2}, Lcom/google/android/gms/common/internal/safeparcel/zzd;->zzai(Landroid/os/Parcel;I)V

    return-void
.end method
