.class synthetic Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment$3;
.super Ljava/lang/Object;
.source "NewsArticleFragment.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x1008
    name = null
.end annotation


# static fields
.field static final synthetic $SwitchMap$net$gogame$gowrap$model$news$MarkupElement$TextStyle:[I


# direct methods
.method static constructor <clinit>()V
    .locals 3

    .line 199
    invoke-static {}, Lnet/gogame/gowrap/model/news/MarkupElement$TextStyle;->values()[Lnet/gogame/gowrap/model/news/MarkupElement$TextStyle;

    move-result-object v0

    array-length v0, v0

    new-array v0, v0, [I

    sput-object v0, Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment$3;->$SwitchMap$net$gogame$gowrap$model$news$MarkupElement$TextStyle:[I

    :try_start_0
    sget-object v0, Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment$3;->$SwitchMap$net$gogame$gowrap$model$news$MarkupElement$TextStyle:[I

    sget-object v1, Lnet/gogame/gowrap/model/news/MarkupElement$TextStyle;->BOLD:Lnet/gogame/gowrap/model/news/MarkupElement$TextStyle;

    invoke-virtual {v1}, Lnet/gogame/gowrap/model/news/MarkupElement$TextStyle;->ordinal()I

    move-result v1

    const/4 v2, 0x1

    aput v2, v0, v1
    :try_end_0
    .catch Ljava/lang/NoSuchFieldError; {:try_start_0 .. :try_end_0} :catch_0

    :catch_0
    :try_start_1
    sget-object v0, Lnet/gogame/gowrap/ui/v2017_2/NewsArticleFragment$3;->$SwitchMap$net$gogame$gowrap$model$news$MarkupElement$TextStyle:[I

    sget-object v1, Lnet/gogame/gowrap/model/news/MarkupElement$TextStyle;->ITALIC:Lnet/gogame/gowrap/model/news/MarkupElement$TextStyle;

    invoke-virtual {v1}, Lnet/gogame/gowrap/model/news/MarkupElement$TextStyle;->ordinal()I

    move-result v1

    const/4 v2, 0x2

    aput v2, v0, v1
    :try_end_1
    .catch Ljava/lang/NoSuchFieldError; {:try_start_1 .. :try_end_1} :catch_1

    :catch_1
    return-void
.end method
