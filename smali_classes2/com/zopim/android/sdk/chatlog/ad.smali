.class Lcom/zopim/android/sdk/chatlog/ad;
.super Ljava/lang/Object;

# interfaces
.implements Lcom/squareup/picasso/Callback;


# instance fields
.field final synthetic a:Lcom/zopim/android/sdk/chatlog/ab;

.field final synthetic b:Lcom/zopim/android/sdk/chatlog/VisitorMessageHolder;


# direct methods
.method constructor <init>(Lcom/zopim/android/sdk/chatlog/VisitorMessageHolder;Lcom/zopim/android/sdk/chatlog/ab;)V
    .locals 0

    iput-object p1, p0, Lcom/zopim/android/sdk/chatlog/ad;->b:Lcom/zopim/android/sdk/chatlog/VisitorMessageHolder;

    iput-object p2, p0, Lcom/zopim/android/sdk/chatlog/ad;->a:Lcom/zopim/android/sdk/chatlog/ab;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onError()V
    .locals 2

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/ad;->b:Lcom/zopim/android/sdk/chatlog/VisitorMessageHolder;

    iget-object v0, v0, Lcom/zopim/android/sdk/chatlog/VisitorMessageHolder;->g:Landroidx/core/widget/ContentLoadingProgressBar;

    const/4 v1, 0x4

    invoke-virtual {v0, v1}, Landroidx/core/widget/ContentLoadingProgressBar;->setVisibility(I)V

    return-void
.end method

.method public onSuccess()V
    .locals 2

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/ad;->b:Lcom/zopim/android/sdk/chatlog/VisitorMessageHolder;

    iget-object v1, p0, Lcom/zopim/android/sdk/chatlog/ad;->a:Lcom/zopim/android/sdk/chatlog/ab;

    iget v1, v1, Lcom/zopim/android/sdk/chatlog/ab;->c:I

    invoke-static {v0, v1}, Lcom/zopim/android/sdk/chatlog/VisitorMessageHolder;->a(Lcom/zopim/android/sdk/chatlog/VisitorMessageHolder;I)V

    return-void
.end method
