.class Lcom/zopim/android/sdk/prechat/h;
.super Ljava/lang/Object;

# interfaces
.implements Ljava/lang/Runnable;


# instance fields
.field final synthetic a:Lcom/zopim/android/sdk/prechat/ZopimChatFragment;


# direct methods
.method constructor <init>(Lcom/zopim/android/sdk/prechat/ZopimChatFragment;)V
    .locals 0

    iput-object p1, p0, Lcom/zopim/android/sdk/prechat/h;->a:Lcom/zopim/android/sdk/prechat/ZopimChatFragment;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 2

    iget-object v0, p0, Lcom/zopim/android/sdk/prechat/h;->a:Lcom/zopim/android/sdk/prechat/ZopimChatFragment;

    invoke-static {v0}, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->access$000(Lcom/zopim/android/sdk/prechat/ZopimChatFragment;)Landroid/view/View;

    move-result-object v0

    const/16 v1, 0x8

    invoke-virtual {v0, v1}, Landroid/view/View;->setVisibility(I)V

    iget-object v0, p0, Lcom/zopim/android/sdk/prechat/h;->a:Lcom/zopim/android/sdk/prechat/ZopimChatFragment;

    invoke-static {v0}, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->access$300(Lcom/zopim/android/sdk/prechat/ZopimChatFragment;)Landroid/view/View;

    move-result-object v0

    invoke-virtual {v0, v1}, Landroid/view/View;->setVisibility(I)V

    iget-object v0, p0, Lcom/zopim/android/sdk/prechat/h;->a:Lcom/zopim/android/sdk/prechat/ZopimChatFragment;

    invoke-static {v0}, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->access$400(Lcom/zopim/android/sdk/prechat/ZopimChatFragment;)Landroid/view/View;

    move-result-object v0

    invoke-virtual {v0, v1}, Landroid/view/View;->setVisibility(I)V

    iget-object v0, p0, Lcom/zopim/android/sdk/prechat/h;->a:Lcom/zopim/android/sdk/prechat/ZopimChatFragment;

    invoke-static {v0}, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->access$500(Lcom/zopim/android/sdk/prechat/ZopimChatFragment;)Landroid/view/View;

    move-result-object v0

    const/4 v1, 0x0

    invoke-virtual {v0, v1}, Landroid/view/View;->setVisibility(I)V

    return-void
.end method
