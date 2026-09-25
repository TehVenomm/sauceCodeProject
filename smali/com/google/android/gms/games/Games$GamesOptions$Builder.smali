.class public final Lcom/google/android/gms/games/Games$GamesOptions$Builder;
.super Ljava/lang/Object;


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/google/android/gms/games/Games$GamesOptions;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x19
    name = "Builder"
.end annotation


# instance fields
.field private zzhcd:Z

.field private zzhce:Z

.field private zzhcf:I

.field private zzhcg:Z

.field private zzhch:I

.field private zzhci:Ljava/lang/String;

.field private zzhcj:Ljava/util/ArrayList;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/ArrayList<",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation
.end field

.field private zzhck:Z

.field private zzhcl:Z

.field private zzhcm:Z


# direct methods
.method private constructor <init>()V
    .locals 2

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    const/4 v0, 0x0

    iput-boolean v0, p0, Lcom/google/android/gms/games/Games$GamesOptions$Builder;->zzhcd:Z

    const/4 v1, 0x1

    iput-boolean v1, p0, Lcom/google/android/gms/games/Games$GamesOptions$Builder;->zzhce:Z

    const/16 v1, 0x11

    iput v1, p0, Lcom/google/android/gms/games/Games$GamesOptions$Builder;->zzhcf:I

    iput-boolean v0, p0, Lcom/google/android/gms/games/Games$GamesOptions$Builder;->zzhcg:Z

    const/16 v1, 0x1110

    iput v1, p0, Lcom/google/android/gms/games/Games$GamesOptions$Builder;->zzhch:I

    const/4 v1, 0x0

    iput-object v1, p0, Lcom/google/android/gms/games/Games$GamesOptions$Builder;->zzhci:Ljava/lang/String;

    new-instance v1, Ljava/util/ArrayList;

    invoke-direct {v1}, Ljava/util/ArrayList;-><init>()V

    iput-object v1, p0, Lcom/google/android/gms/games/Games$GamesOptions$Builder;->zzhcj:Ljava/util/ArrayList;

    iput-boolean v0, p0, Lcom/google/android/gms/games/Games$GamesOptions$Builder;->zzhck:Z

    iput-boolean v0, p0, Lcom/google/android/gms/games/Games$GamesOptions$Builder;->zzhcl:Z

    iput-boolean v0, p0, Lcom/google/android/gms/games/Games$GamesOptions$Builder;->zzhcm:Z

    return-void
.end method

.method synthetic constructor <init>(Lcom/google/android/gms/games/zzb;)V
    .locals 0

    invoke-direct {p0}, Lcom/google/android/gms/games/Games$GamesOptions$Builder;-><init>()V

    return-void
.end method


# virtual methods
.method public final build()Lcom/google/android/gms/games/Games$GamesOptions;
    .locals 13

    new-instance v12, Lcom/google/android/gms/games/Games$GamesOptions;

    iget-boolean v2, p0, Lcom/google/android/gms/games/Games$GamesOptions$Builder;->zzhce:Z

    iget v3, p0, Lcom/google/android/gms/games/Games$GamesOptions$Builder;->zzhcf:I

    iget v5, p0, Lcom/google/android/gms/games/Games$GamesOptions$Builder;->zzhch:I

    iget-object v7, p0, Lcom/google/android/gms/games/Games$GamesOptions$Builder;->zzhcj:Ljava/util/ArrayList;

    const/4 v1, 0x0

    const/4 v4, 0x0

    const/4 v6, 0x0

    const/4 v8, 0x0

    const/4 v9, 0x0

    const/4 v10, 0x0

    const/4 v11, 0x0

    move-object v0, v12

    invoke-direct/range {v0 .. v11}, Lcom/google/android/gms/games/Games$GamesOptions;-><init>(ZZIZILjava/lang/String;Ljava/util/ArrayList;ZZZLcom/google/android/gms/games/zzb;)V

    return-object v12
.end method

.method public final setSdkVariant(I)Lcom/google/android/gms/games/Games$GamesOptions$Builder;
    .locals 0

    iput p1, p0, Lcom/google/android/gms/games/Games$GamesOptions$Builder;->zzhch:I

    return-object p0
.end method

.method public final setShowConnectingPopup(Z)Lcom/google/android/gms/games/Games$GamesOptions$Builder;
    .locals 0

    iput-boolean p1, p0, Lcom/google/android/gms/games/Games$GamesOptions$Builder;->zzhce:Z

    const/16 p1, 0x11

    iput p1, p0, Lcom/google/android/gms/games/Games$GamesOptions$Builder;->zzhcf:I

    return-object p0
.end method

.method public final setShowConnectingPopup(ZI)Lcom/google/android/gms/games/Games$GamesOptions$Builder;
    .locals 0

    iput-boolean p1, p0, Lcom/google/android/gms/games/Games$GamesOptions$Builder;->zzhce:Z

    iput p2, p0, Lcom/google/android/gms/games/Games$GamesOptions$Builder;->zzhcf:I

    return-object p0
.end method
