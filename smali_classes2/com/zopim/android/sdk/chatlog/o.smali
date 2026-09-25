.class Lcom/zopim/android/sdk/chatlog/o;
.super Ljava/lang/Object;

# interfaces
.implements Landroid/view/View$OnClickListener;


# instance fields
.field final synthetic a:Lcom/zopim/android/sdk/chatlog/ChatRatingHolder;


# direct methods
.method constructor <init>(Lcom/zopim/android/sdk/chatlog/ChatRatingHolder;)V
    .locals 0

    iput-object p1, p0, Lcom/zopim/android/sdk/chatlog/o;->a:Lcom/zopim/android/sdk/chatlog/ChatRatingHolder;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onClick(Landroid/view/View;)V
    .locals 1

    iget-object p1, p0, Lcom/zopim/android/sdk/chatlog/o;->a:Lcom/zopim/android/sdk/chatlog/ChatRatingHolder;

    invoke-static {p1}, Lcom/zopim/android/sdk/chatlog/ChatRatingHolder;->a(Lcom/zopim/android/sdk/chatlog/ChatRatingHolder;)Lcom/zopim/android/sdk/chatlog/t;

    move-result-object p1

    iget-object p1, p1, Lcom/zopim/android/sdk/chatlog/t;->a:Lcom/zopim/android/sdk/model/ChatLog$Rating;

    sget-object v0, Lcom/zopim/android/sdk/model/ChatLog$Rating;->GOOD:Lcom/zopim/android/sdk/model/ChatLog$Rating;

    if-ne p1, v0, :cond_0

    iget-object p1, p0, Lcom/zopim/android/sdk/chatlog/o;->a:Lcom/zopim/android/sdk/chatlog/ChatRatingHolder;

    invoke-static {p1}, Lcom/zopim/android/sdk/chatlog/ChatRatingHolder;->b(Lcom/zopim/android/sdk/chatlog/ChatRatingHolder;)Landroid/widget/RadioGroup;

    move-result-object p1

    invoke-virtual {p1}, Landroid/widget/RadioGroup;->clearCheck()V

    iget-object p1, p0, Lcom/zopim/android/sdk/chatlog/o;->a:Lcom/zopim/android/sdk/chatlog/ChatRatingHolder;

    invoke-static {p1}, Lcom/zopim/android/sdk/chatlog/ChatRatingHolder;->c(Lcom/zopim/android/sdk/chatlog/ChatRatingHolder;)Lcom/zopim/android/sdk/chatlog/ChatRatingHolder$Listener;

    move-result-object p1

    if-eqz p1, :cond_1

    iget-object p1, p0, Lcom/zopim/android/sdk/chatlog/o;->a:Lcom/zopim/android/sdk/chatlog/ChatRatingHolder;

    invoke-static {p1}, Lcom/zopim/android/sdk/chatlog/ChatRatingHolder;->c(Lcom/zopim/android/sdk/chatlog/ChatRatingHolder;)Lcom/zopim/android/sdk/chatlog/ChatRatingHolder$Listener;

    move-result-object p1

    sget-object v0, Lcom/zopim/android/sdk/model/ChatLog$Rating;->UNRATED:Lcom/zopim/android/sdk/model/ChatLog$Rating;

    :goto_0
    invoke-interface {p1, v0}, Lcom/zopim/android/sdk/chatlog/ChatRatingHolder$Listener;->onRating(Lcom/zopim/android/sdk/model/ChatLog$Rating;)V

    goto :goto_1

    :cond_0
    iget-object p1, p0, Lcom/zopim/android/sdk/chatlog/o;->a:Lcom/zopim/android/sdk/chatlog/ChatRatingHolder;

    invoke-static {p1}, Lcom/zopim/android/sdk/chatlog/ChatRatingHolder;->c(Lcom/zopim/android/sdk/chatlog/ChatRatingHolder;)Lcom/zopim/android/sdk/chatlog/ChatRatingHolder$Listener;

    move-result-object p1

    if-eqz p1, :cond_1

    iget-object p1, p0, Lcom/zopim/android/sdk/chatlog/o;->a:Lcom/zopim/android/sdk/chatlog/ChatRatingHolder;

    invoke-static {p1}, Lcom/zopim/android/sdk/chatlog/ChatRatingHolder;->c(Lcom/zopim/android/sdk/chatlog/ChatRatingHolder;)Lcom/zopim/android/sdk/chatlog/ChatRatingHolder$Listener;

    move-result-object p1

    sget-object v0, Lcom/zopim/android/sdk/model/ChatLog$Rating;->GOOD:Lcom/zopim/android/sdk/model/ChatLog$Rating;

    goto :goto_0

    :cond_1
    :goto_1
    return-void
.end method
