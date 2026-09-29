.class public final Lcom/google/android/gms/games/Games$GamesOptions;
.super Ljava/lang/Object;

# interfaces
.implements Lcom/google/android/gms/auth/api/signin/GoogleSignInOptionsExtension;
.implements Lcom/google/android/gms/common/api/Api$ApiOptions$Optional;


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/google/android/gms/games/Games;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x19
    name = "GamesOptions"
.end annotation

.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/google/android/gms/games/Games$GamesOptions$Builder;
    }
.end annotation


# instance fields
.field public final zzhcd:Z

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

.field public final zzhcl:Z

.field private zzhcm:Z


# direct methods
.method private constructor <init>(ZZIZILjava/lang/String;Ljava/util/ArrayList;ZZZ)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(ZZIZI",
            "Ljava/lang/String;",
            "Ljava/util/ArrayList<",
            "Ljava/lang/String;",
            ">;ZZZ)V"
        }
    .end annotation

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    iput-boolean p1, p0, Lcom/google/android/gms/games/Games$GamesOptions;->zzhcd:Z

    iput-boolean p2, p0, Lcom/google/android/gms/games/Games$GamesOptions;->zzhce:Z

    iput p3, p0, Lcom/google/android/gms/games/Games$GamesOptions;->zzhcf:I

    iput-boolean p4, p0, Lcom/google/android/gms/games/Games$GamesOptions;->zzhcg:Z

    iput p5, p0, Lcom/google/android/gms/games/Games$GamesOptions;->zzhch:I

    iput-object p6, p0, Lcom/google/android/gms/games/Games$GamesOptions;->zzhci:Ljava/lang/String;

    iput-object p7, p0, Lcom/google/android/gms/games/Games$GamesOptions;->zzhcj:Ljava/util/ArrayList;

    iput-boolean p8, p0, Lcom/google/android/gms/games/Games$GamesOptions;->zzhck:Z

    iput-boolean p9, p0, Lcom/google/android/gms/games/Games$GamesOptions;->zzhcl:Z

    iput-boolean p10, p0, Lcom/google/android/gms/games/Games$GamesOptions;->zzhcm:Z

    return-void
.end method

.method synthetic constructor <init>(ZZIZILjava/lang/String;Ljava/util/ArrayList;ZZZLcom/google/android/gms/games/zzb;)V
    .locals 11

    const/4 v1, 0x0

    const/4 v4, 0x0

    const/4 v6, 0x0

    const/4 v8, 0x0

    const/4 v9, 0x0

    const/4 v10, 0x0

    move-object v0, p0

    move v2, p2

    move v3, p3

    move/from16 v5, p5

    move-object/from16 v7, p7

    invoke-direct/range {v0 .. v10}, Lcom/google/android/gms/games/Games$GamesOptions;-><init>(ZZIZILjava/lang/String;Ljava/util/ArrayList;ZZZ)V

    return-void
.end method

.method public static builder()Lcom/google/android/gms/games/Games$GamesOptions$Builder;
    .locals 2

    new-instance v0, Lcom/google/android/gms/games/Games$GamesOptions$Builder;

    const/4 v1, 0x0

    invoke-direct {v0, v1}, Lcom/google/android/gms/games/Games$GamesOptions$Builder;-><init>(Lcom/google/android/gms/games/zzb;)V

    return-object v0
.end method


# virtual methods
.method public final toBundle()Landroid/os/Bundle;
    .locals 1

    invoke-virtual {p0}, Lcom/google/android/gms/games/Games$GamesOptions;->zzapl()Landroid/os/Bundle;

    move-result-object v0

    return-object v0
.end method

.method public final zzapl()Landroid/os/Bundle;
    .locals 3

    new-instance v0, Landroid/os/Bundle;

    invoke-direct {v0}, Landroid/os/Bundle;-><init>()V

    const-string v1, "com.google.android.gms.games.key.isHeadless"

    iget-boolean v2, p0, Lcom/google/android/gms/games/Games$GamesOptions;->zzhcd:Z

    invoke-virtual {v0, v1, v2}, Landroid/os/Bundle;->putBoolean(Ljava/lang/String;Z)V

    const-string v1, "com.google.android.gms.games.key.showConnectingPopup"

    iget-boolean v2, p0, Lcom/google/android/gms/games/Games$GamesOptions;->zzhce:Z

    invoke-virtual {v0, v1, v2}, Landroid/os/Bundle;->putBoolean(Ljava/lang/String;Z)V

    const-string v1, "com.google.android.gms.games.key.connectingPopupGravity"

    iget v2, p0, Lcom/google/android/gms/games/Games$GamesOptions;->zzhcf:I

    invoke-virtual {v0, v1, v2}, Landroid/os/Bundle;->putInt(Ljava/lang/String;I)V

    const-string v1, "com.google.android.gms.games.key.retryingSignIn"

    iget-boolean v2, p0, Lcom/google/android/gms/games/Games$GamesOptions;->zzhcg:Z

    invoke-virtual {v0, v1, v2}, Landroid/os/Bundle;->putBoolean(Ljava/lang/String;Z)V

    const-string v1, "com.google.android.gms.games.key.sdkVariant"

    iget v2, p0, Lcom/google/android/gms/games/Games$GamesOptions;->zzhch:I

    invoke-virtual {v0, v1, v2}, Landroid/os/Bundle;->putInt(Ljava/lang/String;I)V

    const-string v1, "com.google.android.gms.games.key.forceResolveAccountKey"

    iget-object v2, p0, Lcom/google/android/gms/games/Games$GamesOptions;->zzhci:Ljava/lang/String;

    invoke-virtual {v0, v1, v2}, Landroid/os/Bundle;->putString(Ljava/lang/String;Ljava/lang/String;)V

    const-string v1, "com.google.android.gms.games.key.proxyApis"

    iget-object v2, p0, Lcom/google/android/gms/games/Games$GamesOptions;->zzhcj:Ljava/util/ArrayList;

    invoke-virtual {v0, v1, v2}, Landroid/os/Bundle;->putStringArrayList(Ljava/lang/String;Ljava/util/ArrayList;)V

    const-string v1, "com.google.android.gms.games.key.requireGooglePlus"

    iget-boolean v2, p0, Lcom/google/android/gms/games/Games$GamesOptions;->zzhck:Z

    invoke-virtual {v0, v1, v2}, Landroid/os/Bundle;->putBoolean(Ljava/lang/String;Z)V

    const-string v1, "com.google.android.gms.games.key.unauthenticated"

    iget-boolean v2, p0, Lcom/google/android/gms/games/Games$GamesOptions;->zzhcl:Z

    invoke-virtual {v0, v1, v2}, Landroid/os/Bundle;->putBoolean(Ljava/lang/String;Z)V

    const-string v1, "com.google.android.gms.games.key.skipWelcomePopup"

    iget-boolean v2, p0, Lcom/google/android/gms/games/Games$GamesOptions;->zzhcm:Z

    invoke-virtual {v0, v1, v2}, Landroid/os/Bundle;->putBoolean(Ljava/lang/String;Z)V

    return-object v0
.end method
