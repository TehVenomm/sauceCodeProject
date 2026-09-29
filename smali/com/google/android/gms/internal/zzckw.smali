.class public final Lcom/google/android/gms/internal/zzckw;
.super Lcom/google/android/gms/common/internal/safeparcel/zza;


# static fields
.field public static final CREATOR:Landroid/os/Parcelable$Creator;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Landroid/os/Parcelable$Creator<",
            "Lcom/google/android/gms/internal/zzckw;",
            ">;"
        }
    .end annotation
.end field


# instance fields
.field private final name:Ljava/lang/String;
    .annotation build Landroidx/annotation/Nullable;
    .end annotation
.end field

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

.field private final zzjdc:Lcom/google/android/gms/internal/zzciy;
    .annotation build Landroidx/annotation/Nullable;
    .end annotation
.end field

.field private final zzjdd:Lcom/google/android/gms/internal/zzciv;
    .annotation build Landroidx/annotation/Nullable;
    .end annotation
.end field


# direct methods
.method static constructor <clinit>()V
    .locals 1

    new-instance v0, Lcom/google/android/gms/internal/zzckx;

    invoke-direct {v0}, Lcom/google/android/gms/internal/zzckx;-><init>()V

    sput-object v0, Lcom/google/android/gms/internal/zzckw;->CREATOR:Landroid/os/Parcelable$Creator;

    return-void
.end method

.method public constructor <init>(Landroid/os/IBinder;Landroid/os/IBinder;Landroid/os/IBinder;Ljava/lang/String;Ljava/lang/String;[BLandroid/os/IBinder;)V
    .locals 14
    .param p1    # Landroid/os/IBinder;
        .annotation build Landroidx/annotation/Nullable;
        .end annotation
    .end param
    .param p2    # Landroid/os/IBinder;
        .annotation build Landroidx/annotation/Nullable;
        .end annotation
    .end param
    .param p3    # Landroid/os/IBinder;
        .annotation build Landroidx/annotation/Nullable;
        .end annotation
    .end param
    .param p4    # Ljava/lang/String;
        .annotation build Landroidx/annotation/Nullable;
        .end annotation
    .end param
    .param p6    # [B
        .annotation build Landroidx/annotation/Nullable;
        .end annotation
    .end param
    .param p7    # Landroid/os/IBinder;
        .annotation build Landroidx/annotation/Nullable;
        .end annotation
    .end param

    move-object v0, p1

    move-object/from16 v1, p2

    move-object/from16 v2, p3

    move-object/from16 v3, p7

    const/4 v4, 0x0

    if-nez v0, :cond_0

    move-object v7, v4

    goto :goto_0

    :cond_0
    const-string v5, "com.google.android.gms.nearby.internal.connection.IResultListener"

    invoke-interface {p1, v5}, Landroid/os/IBinder;->queryLocalInterface(Ljava/lang/String;)Landroid/os/IInterface;

    move-result-object v5

    instance-of v6, v5, Lcom/google/android/gms/internal/zzcjl;

    if-eqz v6, :cond_1

    move-object v0, v5

    check-cast v0, Lcom/google/android/gms/internal/zzcjl;

    move-object v7, v0

    goto :goto_0

    :cond_1
    new-instance v5, Lcom/google/android/gms/internal/zzcjn;

    invoke-direct {v5, p1}, Lcom/google/android/gms/internal/zzcjn;-><init>(Landroid/os/IBinder;)V

    move-object v7, v5

    :goto_0
    if-nez v1, :cond_2

    move-object v8, v4

    goto :goto_2

    :cond_2
    const-string v0, "com.google.android.gms.nearby.internal.connection.IConnectionEventListener"

    invoke-interface {v1, v0}, Landroid/os/IBinder;->queryLocalInterface(Ljava/lang/String;)Landroid/os/IInterface;

    move-result-object v0

    instance-of v5, v0, Lcom/google/android/gms/internal/zzcis;

    if-eqz v5, :cond_3

    check-cast v0, Lcom/google/android/gms/internal/zzcis;

    :goto_1
    move-object v8, v0

    goto :goto_2

    :cond_3
    new-instance v0, Lcom/google/android/gms/internal/zzciu;

    invoke-direct {v0, v1}, Lcom/google/android/gms/internal/zzciu;-><init>(Landroid/os/IBinder;)V

    goto :goto_1

    :goto_2
    if-nez v2, :cond_4

    move-object v9, v4

    goto :goto_4

    :cond_4
    const-string v0, "com.google.android.gms.nearby.internal.connection.IConnectionResponseListener"

    invoke-interface {v2, v0}, Landroid/os/IBinder;->queryLocalInterface(Ljava/lang/String;)Landroid/os/IInterface;

    move-result-object v0

    instance-of v1, v0, Lcom/google/android/gms/internal/zzciy;

    if-eqz v1, :cond_5

    check-cast v0, Lcom/google/android/gms/internal/zzciy;

    :goto_3
    move-object v9, v0

    goto :goto_4

    :cond_5
    new-instance v0, Lcom/google/android/gms/internal/zzcja;

    invoke-direct {v0, v2}, Lcom/google/android/gms/internal/zzcja;-><init>(Landroid/os/IBinder;)V

    goto :goto_3

    :goto_4
    if-nez v3, :cond_6

    :goto_5
    move-object v13, v4

    goto :goto_6

    :cond_6
    const-string v0, "com.google.android.gms.nearby.internal.connection.IConnectionLifecycleListener"

    invoke-interface {v3, v0}, Landroid/os/IBinder;->queryLocalInterface(Ljava/lang/String;)Landroid/os/IInterface;

    move-result-object v0

    instance-of v1, v0, Lcom/google/android/gms/internal/zzciv;

    if-eqz v1, :cond_7

    move-object v4, v0

    check-cast v4, Lcom/google/android/gms/internal/zzciv;

    goto :goto_5

    :cond_7
    new-instance v4, Lcom/google/android/gms/internal/zzcix;

    invoke-direct {v4, v3}, Lcom/google/android/gms/internal/zzcix;-><init>(Landroid/os/IBinder;)V

    goto :goto_5

    :goto_6
    move-object v6, p0

    move-object/from16 v10, p4

    move-object/from16 v11, p5

    move-object/from16 v12, p6

    invoke-direct/range {v6 .. v13}, Lcom/google/android/gms/internal/zzckw;-><init>(Lcom/google/android/gms/internal/zzcjl;Lcom/google/android/gms/internal/zzcis;Lcom/google/android/gms/internal/zzciy;Ljava/lang/String;Ljava/lang/String;[BLcom/google/android/gms/internal/zzciv;)V

    return-void
.end method

.method private constructor <init>(Lcom/google/android/gms/internal/zzcjl;Lcom/google/android/gms/internal/zzcis;Lcom/google/android/gms/internal/zzciy;Ljava/lang/String;Ljava/lang/String;[BLcom/google/android/gms/internal/zzciv;)V
    .locals 0
    .param p1    # Lcom/google/android/gms/internal/zzcjl;
        .annotation build Landroidx/annotation/Nullable;
        .end annotation
    .end param
    .param p2    # Lcom/google/android/gms/internal/zzcis;
        .annotation build Landroidx/annotation/Nullable;
        .end annotation
    .end param
    .param p3    # Lcom/google/android/gms/internal/zzciy;
        .annotation build Landroidx/annotation/Nullable;
        .end annotation
    .end param
    .param p4    # Ljava/lang/String;
        .annotation build Landroidx/annotation/Nullable;
        .end annotation
    .end param
    .param p6    # [B
        .annotation build Landroidx/annotation/Nullable;
        .end annotation
    .end param
    .param p7    # Lcom/google/android/gms/internal/zzciv;
        .annotation build Landroidx/annotation/Nullable;
        .end annotation
    .end param

    invoke-direct {p0}, Lcom/google/android/gms/common/internal/safeparcel/zza;-><init>()V

    iput-object p1, p0, Lcom/google/android/gms/internal/zzckw;->zzjaz:Lcom/google/android/gms/internal/zzcjl;

    iput-object p2, p0, Lcom/google/android/gms/internal/zzckw;->zzjba:Lcom/google/android/gms/internal/zzcis;

    iput-object p3, p0, Lcom/google/android/gms/internal/zzckw;->zzjdc:Lcom/google/android/gms/internal/zzciy;

    iput-object p4, p0, Lcom/google/android/gms/internal/zzckw;->name:Ljava/lang/String;

    iput-object p5, p0, Lcom/google/android/gms/internal/zzckw;->zzjbb:Ljava/lang/String;

    iput-object p6, p0, Lcom/google/android/gms/internal/zzckw;->zzjbc:[B

    iput-object p7, p0, Lcom/google/android/gms/internal/zzckw;->zzjdd:Lcom/google/android/gms/internal/zzciv;

    return-void
.end method


# virtual methods
.method public final equals(Ljava/lang/Object;)Z
    .locals 4

    const/4 v0, 0x1

    if-ne p0, p1, :cond_0

    return v0

    :cond_0
    instance-of v1, p1, Lcom/google/android/gms/internal/zzckw;

    const/4 v2, 0x0

    if-eqz v1, :cond_1

    check-cast p1, Lcom/google/android/gms/internal/zzckw;

    iget-object v1, p0, Lcom/google/android/gms/internal/zzckw;->zzjaz:Lcom/google/android/gms/internal/zzcjl;

    iget-object v3, p1, Lcom/google/android/gms/internal/zzckw;->zzjaz:Lcom/google/android/gms/internal/zzcjl;

    invoke-static {v1, v3}, Lcom/google/android/gms/common/internal/zzbf;->equal(Ljava/lang/Object;Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_1

    iget-object v1, p0, Lcom/google/android/gms/internal/zzckw;->zzjba:Lcom/google/android/gms/internal/zzcis;

    iget-object v3, p1, Lcom/google/android/gms/internal/zzckw;->zzjba:Lcom/google/android/gms/internal/zzcis;

    invoke-static {v1, v3}, Lcom/google/android/gms/common/internal/zzbf;->equal(Ljava/lang/Object;Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_1

    iget-object v1, p0, Lcom/google/android/gms/internal/zzckw;->zzjdc:Lcom/google/android/gms/internal/zzciy;

    iget-object v3, p1, Lcom/google/android/gms/internal/zzckw;->zzjdc:Lcom/google/android/gms/internal/zzciy;

    invoke-static {v1, v3}, Lcom/google/android/gms/common/internal/zzbf;->equal(Ljava/lang/Object;Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_1

    iget-object v1, p0, Lcom/google/android/gms/internal/zzckw;->name:Ljava/lang/String;

    iget-object v3, p1, Lcom/google/android/gms/internal/zzckw;->name:Ljava/lang/String;

    invoke-static {v1, v3}, Lcom/google/android/gms/common/internal/zzbf;->equal(Ljava/lang/Object;Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_1

    iget-object v1, p0, Lcom/google/android/gms/internal/zzckw;->zzjbb:Ljava/lang/String;

    iget-object v3, p1, Lcom/google/android/gms/internal/zzckw;->zzjbb:Ljava/lang/String;

    invoke-static {v1, v3}, Lcom/google/android/gms/common/internal/zzbf;->equal(Ljava/lang/Object;Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_1

    iget-object v1, p0, Lcom/google/android/gms/internal/zzckw;->zzjbc:[B

    iget-object v3, p1, Lcom/google/android/gms/internal/zzckw;->zzjbc:[B

    invoke-static {v1, v3}, Lcom/google/android/gms/common/internal/zzbf;->equal(Ljava/lang/Object;Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_1

    iget-object v1, p0, Lcom/google/android/gms/internal/zzckw;->zzjdd:Lcom/google/android/gms/internal/zzciv;

    iget-object p1, p1, Lcom/google/android/gms/internal/zzckw;->zzjdd:Lcom/google/android/gms/internal/zzciv;

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

    iget-object v1, p0, Lcom/google/android/gms/internal/zzckw;->zzjaz:Lcom/google/android/gms/internal/zzcjl;

    const/4 v2, 0x0

    aput-object v1, v0, v2

    iget-object v1, p0, Lcom/google/android/gms/internal/zzckw;->zzjba:Lcom/google/android/gms/internal/zzcis;

    const/4 v2, 0x1

    aput-object v1, v0, v2

    iget-object v1, p0, Lcom/google/android/gms/internal/zzckw;->zzjdc:Lcom/google/android/gms/internal/zzciy;

    const/4 v2, 0x2

    aput-object v1, v0, v2

    iget-object v1, p0, Lcom/google/android/gms/internal/zzckw;->name:Ljava/lang/String;

    const/4 v2, 0x3

    aput-object v1, v0, v2

    iget-object v1, p0, Lcom/google/android/gms/internal/zzckw;->zzjbb:Ljava/lang/String;

    const/4 v2, 0x4

    aput-object v1, v0, v2

    iget-object v1, p0, Lcom/google/android/gms/internal/zzckw;->zzjbc:[B

    const/4 v2, 0x5

    aput-object v1, v0, v2

    iget-object v1, p0, Lcom/google/android/gms/internal/zzckw;->zzjdd:Lcom/google/android/gms/internal/zzciv;

    const/4 v2, 0x6

    aput-object v1, v0, v2

    invoke-static {v0}, Ljava/util/Arrays;->hashCode([Ljava/lang/Object;)I

    move-result v0

    return v0
.end method

.method public final writeToParcel(Landroid/os/Parcel;I)V
    .locals 4

    invoke-static {p1}, Lcom/google/android/gms/common/internal/safeparcel/zzd;->zze(Landroid/os/Parcel;)I

    move-result p2

    iget-object v0, p0, Lcom/google/android/gms/internal/zzckw;->zzjaz:Lcom/google/android/gms/internal/zzcjl;

    const/4 v1, 0x0

    if-nez v0, :cond_0

    move-object v0, v1

    goto :goto_0

    :cond_0
    iget-object v0, p0, Lcom/google/android/gms/internal/zzckw;->zzjaz:Lcom/google/android/gms/internal/zzcjl;

    invoke-interface {v0}, Lcom/google/android/gms/internal/zzcjl;->asBinder()Landroid/os/IBinder;

    move-result-object v0

    :goto_0
    const/4 v2, 0x0

    const/4 v3, 0x1

    invoke-static {p1, v3, v0, v2}, Lcom/google/android/gms/common/internal/safeparcel/zzd;->zza(Landroid/os/Parcel;ILandroid/os/IBinder;Z)V

    const/4 v0, 0x2

    iget-object v3, p0, Lcom/google/android/gms/internal/zzckw;->zzjba:Lcom/google/android/gms/internal/zzcis;

    if-nez v3, :cond_1

    move-object v3, v1

    goto :goto_1

    :cond_1
    iget-object v3, p0, Lcom/google/android/gms/internal/zzckw;->zzjba:Lcom/google/android/gms/internal/zzcis;

    invoke-interface {v3}, Lcom/google/android/gms/internal/zzcis;->asBinder()Landroid/os/IBinder;

    move-result-object v3

    :goto_1
    invoke-static {p1, v0, v3, v2}, Lcom/google/android/gms/common/internal/safeparcel/zzd;->zza(Landroid/os/Parcel;ILandroid/os/IBinder;Z)V

    const/4 v0, 0x3

    iget-object v3, p0, Lcom/google/android/gms/internal/zzckw;->zzjdc:Lcom/google/android/gms/internal/zzciy;

    if-nez v3, :cond_2

    move-object v3, v1

    goto :goto_2

    :cond_2
    iget-object v3, p0, Lcom/google/android/gms/internal/zzckw;->zzjdc:Lcom/google/android/gms/internal/zzciy;

    invoke-interface {v3}, Lcom/google/android/gms/internal/zzciy;->asBinder()Landroid/os/IBinder;

    move-result-object v3

    :goto_2
    invoke-static {p1, v0, v3, v2}, Lcom/google/android/gms/common/internal/safeparcel/zzd;->zza(Landroid/os/Parcel;ILandroid/os/IBinder;Z)V

    const/4 v0, 0x4

    iget-object v3, p0, Lcom/google/android/gms/internal/zzckw;->name:Ljava/lang/String;

    invoke-static {p1, v0, v3, v2}, Lcom/google/android/gms/common/internal/safeparcel/zzd;->zza(Landroid/os/Parcel;ILjava/lang/String;Z)V

    const/4 v0, 0x5

    iget-object v3, p0, Lcom/google/android/gms/internal/zzckw;->zzjbb:Ljava/lang/String;

    invoke-static {p1, v0, v3, v2}, Lcom/google/android/gms/common/internal/safeparcel/zzd;->zza(Landroid/os/Parcel;ILjava/lang/String;Z)V

    const/4 v0, 0x6

    iget-object v3, p0, Lcom/google/android/gms/internal/zzckw;->zzjbc:[B

    invoke-static {p1, v0, v3, v2}, Lcom/google/android/gms/common/internal/safeparcel/zzd;->zza(Landroid/os/Parcel;I[BZ)V

    const/4 v0, 0x7

    iget-object v3, p0, Lcom/google/android/gms/internal/zzckw;->zzjdd:Lcom/google/android/gms/internal/zzciv;

    if-nez v3, :cond_3

    goto :goto_3

    :cond_3
    iget-object v1, p0, Lcom/google/android/gms/internal/zzckw;->zzjdd:Lcom/google/android/gms/internal/zzciv;

    invoke-interface {v1}, Lcom/google/android/gms/internal/zzciv;->asBinder()Landroid/os/IBinder;

    move-result-object v1

    :goto_3
    invoke-static {p1, v0, v1, v2}, Lcom/google/android/gms/common/internal/safeparcel/zzd;->zza(Landroid/os/Parcel;ILandroid/os/IBinder;Z)V

    invoke-static {p1, p2}, Lcom/google/android/gms/common/internal/safeparcel/zzd;->zzai(Landroid/os/Parcel;I)V

    return-void
.end method
