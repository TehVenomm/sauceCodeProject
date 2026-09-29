.class public final enum Lnet/gogame/gowrap/GoWrap$InterstitialAdSize;
.super Ljava/lang/Enum;
.source "GoWrap.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lnet/gogame/gowrap/GoWrap;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x4019
    name = "InterstitialAdSize"
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "Ljava/lang/Enum<",
        "Lnet/gogame/gowrap/GoWrap$InterstitialAdSize;",
        ">;"
    }
.end annotation


# static fields
.field private static final synthetic $VALUES:[Lnet/gogame/gowrap/GoWrap$InterstitialAdSize;

.field public static final enum INTERSTITIAL_AD_SIZE_FULLSCREEN:Lnet/gogame/gowrap/GoWrap$InterstitialAdSize;


# direct methods
.method static constructor <clinit>()V
    .locals 3

    .line 73
    new-instance v0, Lnet/gogame/gowrap/GoWrap$InterstitialAdSize;

    const-string v1, "INTERSTITIAL_AD_SIZE_FULLSCREEN"

    const/4 v2, 0x0

    invoke-direct {v0, v1, v2}, Lnet/gogame/gowrap/GoWrap$InterstitialAdSize;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lnet/gogame/gowrap/GoWrap$InterstitialAdSize;->INTERSTITIAL_AD_SIZE_FULLSCREEN:Lnet/gogame/gowrap/GoWrap$InterstitialAdSize;

    const/4 v0, 0x1

    .line 71
    new-array v0, v0, [Lnet/gogame/gowrap/GoWrap$InterstitialAdSize;

    sget-object v1, Lnet/gogame/gowrap/GoWrap$InterstitialAdSize;->INTERSTITIAL_AD_SIZE_FULLSCREEN:Lnet/gogame/gowrap/GoWrap$InterstitialAdSize;

    aput-object v1, v0, v2

    sput-object v0, Lnet/gogame/gowrap/GoWrap$InterstitialAdSize;->$VALUES:[Lnet/gogame/gowrap/GoWrap$InterstitialAdSize;

    return-void
.end method

.method private constructor <init>(Ljava/lang/String;I)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()V"
        }
    .end annotation

    .line 71
    invoke-direct {p0, p1, p2}, Ljava/lang/Enum;-><init>(Ljava/lang/String;I)V

    return-void
.end method

.method public static valueOf(Ljava/lang/String;)Lnet/gogame/gowrap/GoWrap$InterstitialAdSize;
    .locals 1

    .line 71
    const-class v0, Lnet/gogame/gowrap/GoWrap$InterstitialAdSize;

    invoke-static {v0, p0}, Ljava/lang/Enum;->valueOf(Ljava/lang/Class;Ljava/lang/String;)Ljava/lang/Enum;

    move-result-object p0

    check-cast p0, Lnet/gogame/gowrap/GoWrap$InterstitialAdSize;

    return-object p0
.end method

.method public static values()[Lnet/gogame/gowrap/GoWrap$InterstitialAdSize;
    .locals 1

    .line 71
    sget-object v0, Lnet/gogame/gowrap/GoWrap$InterstitialAdSize;->$VALUES:[Lnet/gogame/gowrap/GoWrap$InterstitialAdSize;

    invoke-virtual {v0}, [Lnet/gogame/gowrap/GoWrap$InterstitialAdSize;->clone()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, [Lnet/gogame/gowrap/GoWrap$InterstitialAdSize;

    return-object v0
.end method
