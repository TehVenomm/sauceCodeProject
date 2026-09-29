.class Lcom/zopim/android/sdk/prechat/q;
.super Landroid/widget/ArrayAdapter;


# annotations
.annotation system Ldalvik/annotation/Signature;
    value = {
        "Landroid/widget/ArrayAdapter<",
        "Ljava/lang/String;",
        ">;"
    }
.end annotation


# instance fields
.field final synthetic a:Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;


# direct methods
.method constructor <init>(Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;Landroid/content/Context;ILjava/util/List;)V
    .locals 0

    iput-object p1, p0, Lcom/zopim/android/sdk/prechat/q;->a:Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;

    invoke-direct {p0, p2, p3, p4}, Landroid/widget/ArrayAdapter;-><init>(Landroid/content/Context;ILjava/util/List;)V

    return-void
.end method


# virtual methods
.method public getCount()I
    .locals 1

    invoke-super {p0}, Landroid/widget/ArrayAdapter;->getCount()I

    move-result v0

    add-int/lit8 v0, v0, -0x1

    return v0
.end method
