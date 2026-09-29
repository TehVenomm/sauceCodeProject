.class Lcom/zopim/android/sdk/chatlog/as;
.super Ljava/lang/Object;

# interfaces
.implements Landroid/view/View$OnClickListener;


# instance fields
.field final synthetic a:Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;


# direct methods
.method constructor <init>(Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;)V
    .locals 0

    iput-object p1, p0, Lcom/zopim/android/sdk/chatlog/as;->a:Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onClick(Landroid/view/View;)V
    .locals 0

    iget-object p1, p0, Lcom/zopim/android/sdk/chatlog/as;->a:Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;

    invoke-virtual {p1}, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->getChildFragmentManager()Landroidx/fragment/app/FragmentManager;

    move-result-object p1

    invoke-static {p1}, Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog;->showDialog(Landroidx/fragment/app/FragmentManager;)V

    return-void
.end method
