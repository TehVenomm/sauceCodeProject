.class public abstract Lnet/gogame/gopay/sdk/a;
.super Landroid/widget/BaseAdapter;


# instance fields
.field public final a:Landroid/content/Context;

.field protected b:Ljava/lang/String;

.field public c:Ljava/util/List;


# direct methods
.method public constructor <init>(Landroid/content/Context;)V
    .locals 0

    invoke-direct {p0}, Landroid/widget/BaseAdapter;-><init>()V

    iput-object p1, p0, Lnet/gogame/gopay/sdk/a;->a:Landroid/content/Context;

    return-void
.end method


# virtual methods
.method public final a(I)I
    .locals 1

    iget-object v0, p0, Lnet/gogame/gopay/sdk/a;->a:Landroid/content/Context;

    int-to-float p1, p1

    invoke-static {v0, p1}, Lnet/gogame/gopay/sdk/support/DisplayUtils;->pxFromDp(Landroid/content/Context;F)I

    move-result p1

    return p1
.end method

.method public final a()Ljava/lang/String;
    .locals 1

    iget-object v0, p0, Lnet/gogame/gopay/sdk/a;->b:Ljava/lang/String;

    return-object v0
.end method

.method public final a(Ljava/lang/String;Ljava/util/List;)V
    .locals 0

    iput-object p1, p0, Lnet/gogame/gopay/sdk/a;->b:Ljava/lang/String;

    iput-object p2, p0, Lnet/gogame/gopay/sdk/a;->c:Ljava/util/List;

    invoke-virtual {p0}, Lnet/gogame/gopay/sdk/a;->notifyDataSetChanged()V

    return-void
.end method

.method public getCount()I
    .locals 1

    iget-object v0, p0, Lnet/gogame/gopay/sdk/a;->c:Ljava/util/List;

    if-nez v0, :cond_0

    const/4 v0, 0x0

    return v0

    :cond_0
    iget-object v0, p0, Lnet/gogame/gopay/sdk/a;->c:Ljava/util/List;

    invoke-interface {v0}, Ljava/util/List;->size()I

    move-result v0

    return v0
.end method

.method public getItem(I)Ljava/lang/Object;
    .locals 1

    iget-object v0, p0, Lnet/gogame/gopay/sdk/a;->c:Ljava/util/List;

    invoke-interface {v0, p1}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object p1

    return-object p1
.end method

.method public getItemId(I)J
    .locals 2

    int-to-long v0, p1

    return-wide v0
.end method
