.class public final Lcom/google/android/gms/internal/zzcla;
.super Lcom/google/android/gms/common/internal/safeparcel/zza;


# static fields
.field public static final CREATOR:Landroid/os/Parcelable$Creator;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Landroid/os/Parcelable$Creator<",
            "Lcom/google/android/gms/internal/zzcla;",
            ">;"
        }
    .end annotation
.end field


# instance fields
.field private final durationMillis:J

.field private final name:Ljava/lang/String;

.field private final zzjan:Ljava/lang/String;

.field private final zzjdd:Lcom/google/android/gms/internal/zzciv;
    .annotation build Landroidx/annotation/Nullable;
    .end annotation
.end field

.field private final zzjdg:Lcom/google/android/gms/internal/zzcjo;
    .annotation build Landroidx/annotation/Nullable;
    .end annotation
.end field

.field private final zzjdh:Lcom/google/android/gms/internal/zzcip;
    .annotation build Landroidx/annotation/Nullable;
    .end annotation
.end field

.field private final zzjdi:Lcom/google/android/gms/nearby/connection/AdvertisingOptions;


# direct methods
.method static constructor <clinit>()V
    .locals 1

    new-instance v0, Lcom/google/android/gms/internal/zzclb;

    invoke-direct {v0}, Lcom/google/android/gms/internal/zzclb;-><init>()V

    sput-object v0, Lcom/google/android/gms/internal/zzcla;->CREATOR:Landroid/os/Parcelable$Creator;

    return-void
.end method

.method public constructor <init>(Landroid/os/IBinder;Landroid/os/IBinder;Ljava/lang/String;Ljava/lang/String;JLcom/google/android/gms/nearby/connection/AdvertisingOptions;Landroid/os/IBinder;)V
    .locals 14
    .param p1    # Landroid/os/IBinder;
        .annotation build Landroidx/annotation/Nullable;
        .end annotation
    .end param
    .param p2    # Landroid/os/IBinder;
        .annotation build Landroidx/annotation/Nullable;
        .end annotation
    .end param
    .param p8    # Landroid/os/IBinder;
        .annotation build Landroidx/annotation/Nullable;
        .end annotation
    .end param

    move-object v0, p1

    move-object/from16 v1, p2

    move-object/from16 v2, p8

    const/4 v3, 0x0

    if-nez v0, :cond_0

    move-object v6, v3

    goto :goto_0

    :cond_0
    const-string v4, "com.google.android.gms.nearby.internal.connection.IStartAdvertisingResultListener"

    invoke-interface {p1, v4}, Landroid/os/IBinder;->queryLocalInterface(Ljava/lang/String;)Landroid/os/IInterface;

    move-result-object v4

    instance-of v5, v4, Lcom/google/android/gms/internal/zzcjo;

    if-eqz v5, :cond_1

    move-object v0, v4

    check-cast v0, Lcom/google/android/gms/internal/zzcjo;

    move-object v6, v0

    goto :goto_0

    :cond_1
    new-instance v4, Lcom/google/android/gms/internal/zzcjq;

    invoke-direct {v4, p1}, Lcom/google/android/gms/internal/zzcjq;-><init>(Landroid/os/IBinder;)V

    move-object v6, v4

    :goto_0
    if-nez v1, :cond_2

    move-object v7, v3

    goto :goto_2

    :cond_2
    const-string v0, "com.google.android.gms.nearby.internal.connection.IAdvertisingCallback"

    invoke-interface {v1, v0}, Landroid/os/IBinder;->queryLocalInterface(Ljava/lang/String;)Landroid/os/IInterface;

    move-result-object v0

    instance-of v4, v0, Lcom/google/android/gms/internal/zzcip;

    if-eqz v4, :cond_3

    check-cast v0, Lcom/google/android/gms/internal/zzcip;

    :goto_1
    move-object v7, v0

    goto :goto_2

    :cond_3
    new-instance v0, Lcom/google/android/gms/internal/zzcir;

    invoke-direct {v0, v1}, Lcom/google/android/gms/internal/zzcir;-><init>(Landroid/os/IBinder;)V

    goto :goto_1

    :goto_2
    if-nez v2, :cond_4

    :goto_3
    move-object v13, v3

    goto :goto_4

    :cond_4
    const-string v0, "com.google.android.gms.nearby.internal.connection.IConnectionLifecycleListener"

    invoke-interface {v2, v0}, Landroid/os/IBinder;->queryLocalInterface(Ljava/lang/String;)Landroid/os/IInterface;

    move-result-object v0

    instance-of v1, v0, Lcom/google/android/gms/internal/zzciv;

    if-eqz v1, :cond_5

    move-object v3, v0

    check-cast v3, Lcom/google/android/gms/internal/zzciv;

    goto :goto_3

    :cond_5
    new-instance v3, Lcom/google/android/gms/internal/zzcix;

    invoke-direct {v3, v2}, Lcom/google/android/gms/internal/zzcix;-><init>(Landroid/os/IBinder;)V

    goto :goto_3

    :goto_4
    move-object v5, p0

    move-object/from16 v8, p3

    move-object/from16 v9, p4

    move-wide/from16 v10, p5

    move-object/from16 v12, p7

    invoke-direct/range {v5 .. v13}, Lcom/google/android/gms/internal/zzcla;-><init>(Lcom/google/android/gms/internal/zzcjo;Lcom/google/android/gms/internal/zzcip;Ljava/lang/String;Ljava/lang/String;JLcom/google/android/gms/nearby/connection/AdvertisingOptions;Lcom/google/android/gms/internal/zzciv;)V

    return-void
.end method

.method private constructor <init>(Lcom/google/android/gms/internal/zzcjo;Lcom/google/android/gms/internal/zzcip;Ljava/lang/String;Ljava/lang/String;JLcom/google/android/gms/nearby/connection/AdvertisingOptions;Lcom/google/android/gms/internal/zzciv;)V
    .locals 0
    .param p1    # Lcom/google/android/gms/internal/zzcjo;
        .annotation build Landroidx/annotation/Nullable;
        .end annotation
    .end param
    .param p2    # Lcom/google/android/gms/internal/zzcip;
        .annotation build Landroidx/annotation/Nullable;
        .end annotation
    .end param
    .param p8    # Lcom/google/android/gms/internal/zzciv;
        .annotation build Landroidx/annotation/Nullable;
        .end annotation
    .end param

    invoke-direct {p0}, Lcom/google/android/gms/common/internal/safeparcel/zza;-><init>()V

    iput-object p1, p0, Lcom/google/android/gms/internal/zzcla;->zzjdg:Lcom/google/android/gms/internal/zzcjo;

    iput-object p2, p0, Lcom/google/android/gms/internal/zzcla;->zzjdh:Lcom/google/android/gms/internal/zzcip;

    iput-object p3, p0, Lcom/google/android/gms/internal/zzcla;->name:Ljava/lang/String;

    iput-object p4, p0, Lcom/google/android/gms/internal/zzcla;->zzjan:Ljava/lang/String;

    iput-wide p5, p0, Lcom/google/android/gms/internal/zzcla;->durationMillis:J

    iput-object p7, p0, Lcom/google/android/gms/internal/zzcla;->zzjdi:Lcom/google/android/gms/nearby/connection/AdvertisingOptions;

    iput-object p8, p0, Lcom/google/android/gms/internal/zzcla;->zzjdd:Lcom/google/android/gms/internal/zzciv;

    return-void
.end method


# virtual methods
.method public final equals(Ljava/lang/Object;)Z
    .locals 5

    const/4 v0, 0x1

    if-ne p0, p1, :cond_0

    return v0

    :cond_0
    instance-of v1, p1, Lcom/google/android/gms/internal/zzcla;

    const/4 v2, 0x0

    if-eqz v1, :cond_1

    check-cast p1, Lcom/google/android/gms/internal/zzcla;

    iget-object v1, p0, Lcom/google/android/gms/internal/zzcla;->zzjdg:Lcom/google/android/gms/internal/zzcjo;

    iget-object v3, p1, Lcom/google/android/gms/internal/zzcla;->zzjdg:Lcom/google/android/gms/internal/zzcjo;

    invoke-static {v1, v3}, Lcom/google/android/gms/common/internal/zzbf;->equal(Ljava/lang/Object;Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_1

    iget-object v1, p0, Lcom/google/android/gms/internal/zzcla;->zzjdh:Lcom/google/android/gms/internal/zzcip;

    iget-object v3, p1, Lcom/google/android/gms/internal/zzcla;->zzjdh:Lcom/google/android/gms/internal/zzcip;

    invoke-static {v1, v3}, Lcom/google/android/gms/common/internal/zzbf;->equal(Ljava/lang/Object;Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_1

    iget-object v1, p0, Lcom/google/android/gms/internal/zzcla;->name:Ljava/lang/String;

    iget-object v3, p1, Lcom/google/android/gms/internal/zzcla;->name:Ljava/lang/String;

    invoke-static {v1, v3}, Lcom/google/android/gms/common/internal/zzbf;->equal(Ljava/lang/Object;Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_1

    iget-object v1, p0, Lcom/google/android/gms/internal/zzcla;->zzjan:Ljava/lang/String;

    iget-object v3, p1, Lcom/google/android/gms/internal/zzcla;->zzjan:Ljava/lang/String;

    invoke-static {v1, v3}, Lcom/google/android/gms/common/internal/zzbf;->equal(Ljava/lang/Object;Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_1

    iget-wide v3, p0, Lcom/google/android/gms/internal/zzcla;->durationMillis:J

    invoke-static {v3, v4}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v1

    iget-wide v3, p1, Lcom/google/android/gms/internal/zzcla;->durationMillis:J

    invoke-static {v3, v4}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v3

    invoke-static {v1, v3}, Lcom/google/android/gms/common/internal/zzbf;->equal(Ljava/lang/Object;Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_1

    iget-object v1, p0, Lcom/google/android/gms/internal/zzcla;->zzjdi:Lcom/google/android/gms/nearby/connection/AdvertisingOptions;

    iget-object v3, p1, Lcom/google/android/gms/internal/zzcla;->zzjdi:Lcom/google/android/gms/nearby/connection/AdvertisingOptions;

    invoke-static {v1, v3}, Lcom/google/android/gms/common/internal/zzbf;->equal(Ljava/lang/Object;Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_1

    iget-object v1, p0, Lcom/google/android/gms/internal/zzcla;->zzjdd:Lcom/google/android/gms/internal/zzciv;

    iget-object p1, p1, Lcom/google/android/gms/internal/zzcla;->zzjdd:Lcom/google/android/gms/internal/zzciv;

    invoke-static {v1, p1}, Lcom/google/android/gms/common/internal/zzbf;->equal(Ljava/lang/Object;Ljava/lang/Object;)Z

    move-result p1

    if-eqz p1, :cond_1

    return v0

    :cond_1
    return v2
.end method

.method public final hashCode()I
    .locals 3

    const/4 v0, 0x7

    new-array v0, v0, [Ljava/lang/Object;

    iget-object v1, p0, Lcom/google/android/gms/internal/zzcla;->zzjdg:Lcom/google/android/gms/internal/zzcjo;

    const/4 v2, 0x0

    aput-object v1, v0, v2

    iget-object v1, p0, Lcom/google/android/gms/internal/zzcla;->zzjdh:Lcom/google/android/gms/internal/zzcip;

    const/4 v2, 0x1

    aput-object v1, v0, v2

    iget-object v1, p0, Lcom/google/android/gms/internal/zzcla;->name:Ljava/lang/String;

    const/4 v2, 0x2

    aput-object v1, v0, v2

    iget-object v1, p0, Lcom/google/android/gms/internal/zzcla;->zzjan:Ljava/lang/String;

    const/4 v2, 0x3

    aput-object v1, v0, v2

    iget-wide v1, p0, Lcom/google/android/gms/internal/zzcla;->durationMillis:J

    invoke-static {v1, v2}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v1

    const/4 v2, 0x4

    aput-object v1, v0, v2

    iget-object v1, p0, Lcom/google/android/gms/internal/zzcla;->zzjdi:Lcom/google/android/gms/nearby/connection/AdvertisingOptions;

    const/4 v2, 0x5

    aput-object v1, v0, v2

    iget-object v1, p0, Lcom/google/android/gms/internal/zzcla;->zzjdd:Lcom/google/android/gms/internal/zzciv;

    const/4 v2, 0x6

    aput-object v1, v0, v2

    invoke-static {v0}, Ljava/util/Arrays;->hashCode([Ljava/lang/Object;)I

    move-result v0

    return v0
.end method

.method public final writeToParcel(Landroid/os/Parcel;I)V
    .locals 6

    invoke-static {p1}, Lcom/google/android/gms/common/internal/safeparcel/zzd;->zze(Landroid/os/Parcel;)I

    move-result v0

    iget-object v1, p0, Lcom/google/android/gms/internal/zzcla;->zzjdg:Lcom/google/android/gms/internal/zzcjo;

    const/4 v2, 0x0

    if-nez v1, :cond_0

    move-object v1, v2

    goto :goto_0

    :cond_0
    iget-object v1, p0, Lcom/google/android/gms/internal/zzcla;->zzjdg:Lcom/google/android/gms/internal/zzcjo;

    invoke-interface {v1}, Lcom/google/android/gms/internal/zzcjo;->asBinder()Landroid/os/IBinder;

    move-result-object v1

    :goto_0
    const/4 v3, 0x0

    const/4 v4, 0x1

    invoke-static {p1, v4, v1, v3}, Lcom/google/android/gms/common/internal/safeparcel/zzd;->zza(Landroid/os/Parcel;ILandroid/os/IBinder;Z)V

    const/4 v1, 0x2

    iget-object v4, p0, Lcom/google/android/gms/internal/zzcla;->zzjdh:Lcom/google/android/gms/internal/zzcip;

    if-nez v4, :cond_1

    move-object v4, v2

    goto :goto_1

    :cond_1
    iget-object v4, p0, Lcom/google/android/gms/internal/zzcla;->zzjdh:Lcom/google/android/gms/internal/zzcip;

    invoke-interface {v4}, Lcom/google/android/gms/internal/zzcip;->asBinder()Landroid/os/IBinder;

    move-result-object v4

    :goto_1
    invoke-static {p1, v1, v4, v3}, Lcom/google/android/gms/common/internal/safeparcel/zzd;->zza(Landroid/os/Parcel;ILandroid/os/IBinder;Z)V

    const/4 v1, 0x3

    iget-object v4, p0, Lcom/google/android/gms/internal/zzcla;->name:Ljava/lang/String;

    invoke-static {p1, v1, v4, v3}, Lcom/google/android/gms/common/internal/safeparcel/zzd;->zza(Landroid/os/Parcel;ILjava/lang/String;Z)V

    const/4 v1, 0x4

    iget-object v4, p0, Lcom/google/android/gms/internal/zzcla;->zzjan:Ljava/lang/String;

    invoke-static {p1, v1, v4, v3}, Lcom/google/android/gms/common/internal/safeparcel/zzd;->zza(Landroid/os/Parcel;ILjava/lang/String;Z)V

    const/4 v1, 0x5

    iget-wide v4, p0, Lcom/google/android/gms/internal/zzcla;->durationMillis:J

    invoke-static {p1, v1, v4, v5}, Lcom/google/android/gms/common/internal/safeparcel/zzd;->zza(Landroid/os/Parcel;IJ)V

    const/4 v1, 0x6

    iget-object v4, p0, Lcom/google/android/gms/internal/zzcla;->zzjdi:Lcom/google/android/gms/nearby/connection/AdvertisingOptions;

    invoke-static {p1, v1, v4, p2, v3}, Lcom/google/android/gms/common/internal/safeparcel/zzd;->zza(Landroid/os/Parcel;ILandroid/os/Parcelable;IZ)V

    const/4 p2, 0x7

    iget-object v1, p0, Lcom/google/android/gms/internal/zzcla;->zzjdd:Lcom/google/android/gms/internal/zzciv;

    if-nez v1, :cond_2

    goto :goto_2

    :cond_2
    iget-object v1, p0, Lcom/google/android/gms/internal/zzcla;->zzjdd:Lcom/google/android/gms/internal/zzciv;

    invoke-interface {v1}, Lcom/google/android/gms/internal/zzciv;->asBinder()Landroid/os/IBinder;

    move-result-object v2

    :goto_2
    invoke-static {p1, p2, v2, v3}, Lcom/google/android/gms/common/internal/safeparcel/zzd;->zza(Landroid/os/Parcel;ILandroid/os/IBinder;Z)V

    invoke-static {p1, v0}, Lcom/google/android/gms/common/internal/safeparcel/zzd;->zzai(Landroid/os/Parcel;I)V

    return-void
.end method
