.class Lcom/zopim/android/sdk/prechat/n;
.super Ljava/lang/Object;

# interfaces
.implements Landroid/content/DialogInterface$OnClickListener;


# instance fields
.field final synthetic a:Lcom/zopim/android/sdk/prechat/l;


# direct methods
.method constructor <init>(Lcom/zopim/android/sdk/prechat/l;)V
    .locals 0

    iput-object p1, p0, Lcom/zopim/android/sdk/prechat/n;->a:Lcom/zopim/android/sdk/prechat/l;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onClick(Landroid/content/DialogInterface;I)V
    .locals 0

    iget-object p1, p0, Lcom/zopim/android/sdk/prechat/n;->a:Lcom/zopim/android/sdk/prechat/l;

    iget-object p1, p1, Lcom/zopim/android/sdk/prechat/l;->a:Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;

    invoke-static {p1}, Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;->access$500(Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;)V

    return-void
.end method
