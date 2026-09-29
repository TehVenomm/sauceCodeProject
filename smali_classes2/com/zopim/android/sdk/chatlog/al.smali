.class Lcom/zopim/android/sdk/chatlog/al;
.super Ljava/lang/Object;

# interfaces
.implements Landroid/content/DialogInterface$OnClickListener;


# instance fields
.field final synthetic a:Lcom/zopim/android/sdk/chatlog/aj;


# direct methods
.method constructor <init>(Lcom/zopim/android/sdk/chatlog/aj;)V
    .locals 0

    iput-object p1, p0, Lcom/zopim/android/sdk/chatlog/al;->a:Lcom/zopim/android/sdk/chatlog/aj;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onClick(Landroid/content/DialogInterface;I)V
    .locals 0

    invoke-interface {p1}, Landroid/content/DialogInterface;->dismiss()V

    return-void
.end method
