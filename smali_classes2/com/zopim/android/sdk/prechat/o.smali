.class Lcom/zopim/android/sdk/prechat/o;
.super Lcom/zopim/android/sdk/data/observers/FormsObserver;


# instance fields
.field final synthetic a:Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;


# direct methods
.method constructor <init>(Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;)V
    .locals 0

    iput-object p1, p0, Lcom/zopim/android/sdk/prechat/o;->a:Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;

    invoke-direct {p0}, Lcom/zopim/android/sdk/data/observers/FormsObserver;-><init>()V

    return-void
.end method


# virtual methods
.method public update(Lcom/zopim/android/sdk/model/Forms;)V
    .locals 1

    invoke-virtual {p1}, Lcom/zopim/android/sdk/model/Forms;->getOfflineForm()Lcom/zopim/android/sdk/model/Forms$OfflineForm;

    move-result-object p1

    if-eqz p1, :cond_0

    invoke-virtual {p1}, Lcom/zopim/android/sdk/model/Forms$OfflineForm;->getFormSubmitted()Lcom/zopim/android/sdk/model/Forms$FormSubmitted;

    move-result-object p1

    if-nez p1, :cond_0

    iget-object p1, p0, Lcom/zopim/android/sdk/prechat/o;->a:Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;

    invoke-static {p1}, Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;->access$600(Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;)Landroid/os/Handler;

    move-result-object p1

    new-instance v0, Lcom/zopim/android/sdk/prechat/p;

    invoke-direct {v0, p0}, Lcom/zopim/android/sdk/prechat/p;-><init>(Lcom/zopim/android/sdk/prechat/o;)V

    invoke-virtual {p1, v0}, Landroid/os/Handler;->post(Ljava/lang/Runnable;)Z

    :cond_0
    return-void
.end method
