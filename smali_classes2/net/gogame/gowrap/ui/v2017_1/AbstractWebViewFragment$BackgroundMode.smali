.class public final enum Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$BackgroundMode;
.super Ljava/lang/Enum;
.source "AbstractWebViewFragment.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x401c
    name = "BackgroundMode"
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "Ljava/lang/Enum<",
        "Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$BackgroundMode;",
        ">;"
    }
.end annotation


# static fields
.field private static final synthetic $VALUES:[Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$BackgroundMode;

.field public static final enum DEFAULT:Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$BackgroundMode;

.field public static final enum TRANSPARENT:Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$BackgroundMode;


# direct methods
.method static constructor <clinit>()V
    .locals 4

    .line 510
    new-instance v0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$BackgroundMode;

    const-string v1, "DEFAULT"

    const/4 v2, 0x0

    invoke-direct {v0, v1, v2}, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$BackgroundMode;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$BackgroundMode;->DEFAULT:Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$BackgroundMode;

    .line 511
    new-instance v0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$BackgroundMode;

    const-string v1, "TRANSPARENT"

    const/4 v3, 0x1

    invoke-direct {v0, v1, v3}, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$BackgroundMode;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$BackgroundMode;->TRANSPARENT:Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$BackgroundMode;

    const/4 v0, 0x2

    .line 509
    new-array v0, v0, [Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$BackgroundMode;

    sget-object v1, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$BackgroundMode;->DEFAULT:Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$BackgroundMode;

    aput-object v1, v0, v2

    sget-object v1, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$BackgroundMode;->TRANSPARENT:Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$BackgroundMode;

    aput-object v1, v0, v3

    sput-object v0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$BackgroundMode;->$VALUES:[Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$BackgroundMode;

    return-void
.end method

.method private constructor <init>(Ljava/lang/String;I)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()V"
        }
    .end annotation

    .line 509
    invoke-direct {p0, p1, p2}, Ljava/lang/Enum;-><init>(Ljava/lang/String;I)V

    return-void
.end method

.method public static valueOf(Ljava/lang/String;)Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$BackgroundMode;
    .locals 1

    .line 509
    const-class v0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$BackgroundMode;

    invoke-static {v0, p0}, Ljava/lang/Enum;->valueOf(Ljava/lang/Class;Ljava/lang/String;)Ljava/lang/Enum;

    move-result-object p0

    check-cast p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$BackgroundMode;

    return-object p0
.end method

.method public static values()[Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$BackgroundMode;
    .locals 1

    .line 509
    sget-object v0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$BackgroundMode;->$VALUES:[Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$BackgroundMode;

    invoke-virtual {v0}, [Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$BackgroundMode;->clone()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, [Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$BackgroundMode;

    return-object v0
.end method
