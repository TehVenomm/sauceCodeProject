.class public Lcom/google/android/gms/nearby/messages/MessageFilter;
.super Lcom/google/android/gms/common/internal/safeparcel/zza;


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/google/android/gms/nearby/messages/MessageFilter$Builder;
    }
.end annotation


# static fields
.field public static final CREATOR:Landroid/os/Parcelable$Creator;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Landroid/os/Parcelable$Creator<",
            "Lcom/google/android/gms/nearby/messages/MessageFilter;",
            ">;"
        }
    .end annotation
.end field

.field public static final INCLUDE_ALL_MY_TYPES:Lcom/google/android/gms/nearby/messages/MessageFilter;


# instance fields
.field private zzdxt:I

.field private final zzjds:Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/List<",
            "Lcom/google/android/gms/nearby/messages/internal/zzad;",
            ">;"
        }
    .end annotation
.end field

.field private final zzjdt:Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/List<",
            "Lcom/google/android/gms/internal/zzclo;",
            ">;"
        }
    .end annotation
.end field

.field private final zzjdu:Z

.field private final zzjdv:Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/List<",
            "Lcom/google/android/gms/internal/zzclk;",
            ">;"
        }
    .end annotation
.end field

.field private final zzjdw:I


# direct methods
.method static constructor <clinit>()V
    .locals 1

    new-instance v0, Lcom/google/android/gms/nearby/messages/zzc;

    invoke-direct {v0}, Lcom/google/android/gms/nearby/messages/zzc;-><init>()V

    sput-object v0, Lcom/google/android/gms/nearby/messages/MessageFilter;->CREATOR:Landroid/os/Parcelable$Creator;

    new-instance v0, Lcom/google/android/gms/nearby/messages/MessageFilter$Builder;

    invoke-direct {v0}, Lcom/google/android/gms/nearby/messages/MessageFilter$Builder;-><init>()V

    invoke-virtual {v0}, Lcom/google/android/gms/nearby/messages/MessageFilter$Builder;->includeAllMyTypes()Lcom/google/android/gms/nearby/messages/MessageFilter$Builder;

    move-result-object v0

    invoke-virtual {v0}, Lcom/google/android/gms/nearby/messages/MessageFilter$Builder;->build()Lcom/google/android/gms/nearby/messages/MessageFilter;

    move-result-object v0

    sput-object v0, Lcom/google/android/gms/nearby/messages/MessageFilter;->INCLUDE_ALL_MY_TYPES:Lcom/google/android/gms/nearby/messages/MessageFilter;

    return-void
.end method

.method constructor <init>(ILjava/util/List;Ljava/util/List;ZLjava/util/List;I)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(I",
            "Ljava/util/List<",
            "Lcom/google/android/gms/nearby/messages/internal/zzad;",
            ">;",
            "Ljava/util/List<",
            "Lcom/google/android/gms/internal/zzclo;",
            ">;Z",
            "Ljava/util/List<",
            "Lcom/google/android/gms/internal/zzclk;",
            ">;I)V"
        }
    .end annotation

    invoke-direct {p0}, Lcom/google/android/gms/common/internal/safeparcel/zza;-><init>()V

    iput p1, p0, Lcom/google/android/gms/nearby/messages/MessageFilter;->zzdxt:I

    invoke-static {p2}, Lcom/google/android/gms/common/internal/zzbp;->zzu(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Ljava/util/List;

    invoke-static {p1}, Ljava/util/Collections;->unmodifiableList(Ljava/util/List;)Ljava/util/List;

    move-result-object p1

    iput-object p1, p0, Lcom/google/android/gms/nearby/messages/MessageFilter;->zzjds:Ljava/util/List;

    iput-boolean p4, p0, Lcom/google/android/gms/nearby/messages/MessageFilter;->zzjdu:Z

    if-nez p3, :cond_0

    invoke-static {}, Ljava/util/Collections;->emptyList()Ljava/util/List;

    move-result-object p3

    :cond_0
    invoke-static {p3}, Ljava/util/Collections;->unmodifiableList(Ljava/util/List;)Ljava/util/List;

    move-result-object p1

    iput-object p1, p0, Lcom/google/android/gms/nearby/messages/MessageFilter;->zzjdt:Ljava/util/List;

    if-nez p5, :cond_1

    invoke-static {}, Ljava/util/Collections;->emptyList()Ljava/util/List;

    move-result-object p5

    :cond_1
    invoke-static {p5}, Ljava/util/Collections;->unmodifiableList(Ljava/util/List;)Ljava/util/List;

    move-result-object p1

    iput-object p1, p0, Lcom/google/android/gms/nearby/messages/MessageFilter;->zzjdv:Ljava/util/List;

    iput p6, p0, Lcom/google/android/gms/nearby/messages/MessageFilter;->zzjdw:I

    return-void
.end method

.method private constructor <init>(Ljava/util/List;Ljava/util/List;ZLjava/util/List;I)V
    .locals 7
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lcom/google/android/gms/nearby/messages/internal/zzad;",
            ">;",
            "Ljava/util/List<",
            "Lcom/google/android/gms/internal/zzclo;",
            ">;Z",
            "Ljava/util/List<",
            "Lcom/google/android/gms/internal/zzclk;",
            ">;I)V"
        }
    .end annotation

    const/4 v1, 0x2

    move-object v0, p0

    move-object v2, p1

    move-object v3, p2

    move v4, p3

    move-object v5, p4

    move v6, p5

    invoke-direct/range {v0 .. v6}, Lcom/google/android/gms/nearby/messages/MessageFilter;-><init>(ILjava/util/List;Ljava/util/List;ZLjava/util/List;I)V

    return-void
.end method

.method synthetic constructor <init>(Ljava/util/List;Ljava/util/List;ZLjava/util/List;ILcom/google/android/gms/nearby/messages/zzb;)V
    .locals 0

    invoke-direct/range {p0 .. p5}, Lcom/google/android/gms/nearby/messages/MessageFilter;-><init>(Ljava/util/List;Ljava/util/List;ZLjava/util/List;I)V

    return-void
.end method


# virtual methods
.method public equals(Ljava/lang/Object;)Z
    .locals 4

    const/4 v0, 0x1

    if-ne p0, p1, :cond_0

    return v0

    :cond_0
    instance-of v1, p1, Lcom/google/android/gms/nearby/messages/MessageFilter;

    const/4 v2, 0x0

    if-nez v1, :cond_1

    return v2

    :cond_1
    check-cast p1, Lcom/google/android/gms/nearby/messages/MessageFilter;

    iget-boolean v1, p0, Lcom/google/android/gms/nearby/messages/MessageFilter;->zzjdu:Z

    iget-boolean v3, p1, Lcom/google/android/gms/nearby/messages/MessageFilter;->zzjdu:Z

    if-ne v1, v3, :cond_2

    iget-object v1, p0, Lcom/google/android/gms/nearby/messages/MessageFilter;->zzjds:Ljava/util/List;

    iget-object v3, p1, Lcom/google/android/gms/nearby/messages/MessageFilter;->zzjds:Ljava/util/List;

    invoke-static {v1, v3}, Lcom/google/android/gms/common/internal/zzbf;->equal(Ljava/lang/Object;Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_2

    iget-object v1, p0, Lcom/google/android/gms/nearby/messages/MessageFilter;->zzjdt:Ljava/util/List;

    iget-object v3, p1, Lcom/google/android/gms/nearby/messages/MessageFilter;->zzjdt:Ljava/util/List;

    invoke-static {v1, v3}, Lcom/google/android/gms/common/internal/zzbf;->equal(Ljava/lang/Object;Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_2

    iget-object v1, p0, Lcom/google/android/gms/nearby/messages/MessageFilter;->zzjdv:Ljava/util/List;

    iget-object p1, p1, Lcom/google/android/gms/nearby/messages/MessageFilter;->zzjdv:Ljava/util/List;

    invoke-static {v1, p1}, Lcom/google/android/gms/common/internal/zzbf;->equal(Ljava/lang/Object;Ljava/lang/Object;)Z

    move-result p1

    if-eqz p1, :cond_2

    return v0

    :cond_2
    return v2
.end method

.method public hashCode()I
    .locals 3

    const/4 v0, 0x4

    new-array v0, v0, [Ljava/lang/Object;

    iget-object v1, p0, Lcom/google/android/gms/nearby/messages/MessageFilter;->zzjds:Ljava/util/List;

    const/4 v2, 0x0

    aput-object v1, v0, v2

    iget-object v1, p0, Lcom/google/android/gms/nearby/messages/MessageFilter;->zzjdt:Ljava/util/List;

    const/4 v2, 0x1

    aput-object v1, v0, v2

    iget-boolean v1, p0, Lcom/google/android/gms/nearby/messages/MessageFilter;->zzjdu:Z

    invoke-static {v1}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v1

    const/4 v2, 0x2

    aput-object v1, v0, v2

    iget-object v1, p0, Lcom/google/android/gms/nearby/messages/MessageFilter;->zzjdv:Ljava/util/List;

    const/4 v2, 0x3

    aput-object v1, v0, v2

    invoke-static {v0}, Ljava/util/Arrays;->hashCode([Ljava/lang/Object;)I

    move-result v0

    return v0
.end method

.method public toString()Ljava/lang/String;
    .locals 4

    iget-boolean v0, p0, Lcom/google/android/gms/nearby/messages/MessageFilter;->zzjdu:Z

    iget-object v1, p0, Lcom/google/android/gms/nearby/messages/MessageFilter;->zzjds:Ljava/util/List;

    invoke-static {v1}, Ljava/lang/String;->valueOf(Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v1

    invoke-static {v1}, Ljava/lang/String;->valueOf(Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v2}, Ljava/lang/String;->length()I

    move-result v2

    add-int/lit8 v2, v2, 0x35

    new-instance v3, Ljava/lang/StringBuilder;

    invoke-direct {v3, v2}, Ljava/lang/StringBuilder;-><init>(I)V

    const-string v2, "MessageFilter{includeAllMyTypes="

    invoke-virtual {v3, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v3, v0}, Ljava/lang/StringBuilder;->append(Z)Ljava/lang/StringBuilder;

    const-string v0, ", messageTypes="

    invoke-virtual {v3, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v3, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v0, "}"

    invoke-virtual {v3, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method public writeToParcel(Landroid/os/Parcel;I)V
    .locals 3

    invoke-static {p1}, Lcom/google/android/gms/common/internal/safeparcel/zzd;->zze(Landroid/os/Parcel;)I

    move-result p2

    iget-object v0, p0, Lcom/google/android/gms/nearby/messages/MessageFilter;->zzjds:Ljava/util/List;

    const/4 v1, 0x0

    const/4 v2, 0x1

    invoke-static {p1, v2, v0, v1}, Lcom/google/android/gms/common/internal/safeparcel/zzd;->zzc(Landroid/os/Parcel;ILjava/util/List;Z)V

    iget-object v0, p0, Lcom/google/android/gms/nearby/messages/MessageFilter;->zzjdt:Ljava/util/List;

    const/4 v2, 0x2

    invoke-static {p1, v2, v0, v1}, Lcom/google/android/gms/common/internal/safeparcel/zzd;->zzc(Landroid/os/Parcel;ILjava/util/List;Z)V

    iget-boolean v0, p0, Lcom/google/android/gms/nearby/messages/MessageFilter;->zzjdu:Z

    const/4 v2, 0x3

    invoke-static {p1, v2, v0}, Lcom/google/android/gms/common/internal/safeparcel/zzd;->zza(Landroid/os/Parcel;IZ)V

    iget-object v0, p0, Lcom/google/android/gms/nearby/messages/MessageFilter;->zzjdv:Ljava/util/List;

    const/4 v2, 0x4

    invoke-static {p1, v2, v0, v1}, Lcom/google/android/gms/common/internal/safeparcel/zzd;->zzc(Landroid/os/Parcel;ILjava/util/List;Z)V

    iget v0, p0, Lcom/google/android/gms/nearby/messages/MessageFilter;->zzjdw:I

    const/4 v1, 0x5

    invoke-static {p1, v1, v0}, Lcom/google/android/gms/common/internal/safeparcel/zzd;->zzc(Landroid/os/Parcel;II)V

    iget v0, p0, Lcom/google/android/gms/nearby/messages/MessageFilter;->zzdxt:I

    const/16 v1, 0x3e8

    invoke-static {p1, v1, v0}, Lcom/google/android/gms/common/internal/safeparcel/zzd;->zzc(Landroid/os/Parcel;II)V

    invoke-static {p1, p2}, Lcom/google/android/gms/common/internal/safeparcel/zzd;->zzai(Landroid/os/Parcel;I)V

    return-void
.end method

.method public final zzbau()Ljava/util/List;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/List<",
            "Lcom/google/android/gms/nearby/messages/internal/zzad;",
            ">;"
        }
    .end annotation

    iget-object v0, p0, Lcom/google/android/gms/nearby/messages/MessageFilter;->zzjds:Ljava/util/List;

    return-object v0
.end method

.method public final zzbav()Z
    .locals 1

    iget-boolean v0, p0, Lcom/google/android/gms/nearby/messages/MessageFilter;->zzjdu:Z

    return v0
.end method

.method final zzbaw()Ljava/util/List;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/List<",
            "Lcom/google/android/gms/internal/zzclo;",
            ">;"
        }
    .end annotation

    iget-object v0, p0, Lcom/google/android/gms/nearby/messages/MessageFilter;->zzjdt:Ljava/util/List;

    return-object v0
.end method

.method public final zzbax()Ljava/util/List;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/List<",
            "Lcom/google/android/gms/internal/zzclk;",
            ">;"
        }
    .end annotation

    iget-object v0, p0, Lcom/google/android/gms/nearby/messages/MessageFilter;->zzjdv:Ljava/util/List;

    return-object v0
.end method
