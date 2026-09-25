.class synthetic Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$4;
.super Ljava/lang/Object;
.source "AbstractWebViewFragment.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x1008
    name = null
.end annotation


# static fields
.field static final synthetic $SwitchMap$net$gogame$gowrap$ui$v2017_1$AbstractWebViewFragment$BackgroundMode:[I


# direct methods
.method static constructor <clinit>()V
    .locals 3

    .line 323
    invoke-static {}, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$BackgroundMode;->values()[Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$BackgroundMode;

    move-result-object v0

    array-length v0, v0

    new-array v0, v0, [I

    sput-object v0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$4;->$SwitchMap$net$gogame$gowrap$ui$v2017_1$AbstractWebViewFragment$BackgroundMode:[I

    :try_start_0
    sget-object v0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$4;->$SwitchMap$net$gogame$gowrap$ui$v2017_1$AbstractWebViewFragment$BackgroundMode:[I

    sget-object v1, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$BackgroundMode;->TRANSPARENT:Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$BackgroundMode;

    invoke-virtual {v1}, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$BackgroundMode;->ordinal()I

    move-result v1

    const/4 v2, 0x1

    aput v2, v0, v1
    :try_end_0
    .catch Ljava/lang/NoSuchFieldError; {:try_start_0 .. :try_end_0} :catch_0

    :catch_0
    :try_start_1
    sget-object v0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$4;->$SwitchMap$net$gogame$gowrap$ui$v2017_1$AbstractWebViewFragment$BackgroundMode:[I

    sget-object v1, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$BackgroundMode;->DEFAULT:Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$BackgroundMode;

    invoke-virtual {v1}, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$BackgroundMode;->ordinal()I

    move-result v1

    const/4 v2, 0x2

    aput v2, v0, v1
    :try_end_1
    .catch Ljava/lang/NoSuchFieldError; {:try_start_1 .. :try_end_1} :catch_1

    :catch_1
    return-void
.end method
