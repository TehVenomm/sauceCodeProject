.class final Lnet/gogame/gopay/sdk/o;
.super Ljava/lang/Object;

# interfaces
.implements Landroid/widget/AdapterView$OnItemSelectedListener;


# instance fields
.field final synthetic a:Lnet/gogame/gopay/sdk/StoreActivity;


# direct methods
.method constructor <init>(Lnet/gogame/gopay/sdk/StoreActivity;)V
    .locals 0

    iput-object p1, p0, Lnet/gogame/gopay/sdk/o;->a:Lnet/gogame/gopay/sdk/StoreActivity;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public final onItemSelected(Landroid/widget/AdapterView;Landroid/view/View;IJ)V
    .locals 0

    iget-object p1, p0, Lnet/gogame/gopay/sdk/o;->a:Lnet/gogame/gopay/sdk/StoreActivity;

    invoke-static {p1}, Lnet/gogame/gopay/sdk/StoreActivity;->a(Lnet/gogame/gopay/sdk/StoreActivity;)Lnet/gogame/gopay/sdk/d;

    move-result-object p1

    invoke-virtual {p1, p3}, Lnet/gogame/gopay/sdk/d;->getItem(I)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Lnet/gogame/gopay/sdk/Country;

    invoke-virtual {p1}, Lnet/gogame/gopay/sdk/Country;->getCode()Ljava/lang/String;

    move-result-object p1

    invoke-static {p1}, Lnet/gogame/gopay/sdk/j;->a(Ljava/lang/String;)V

    iget-object p1, p0, Lnet/gogame/gopay/sdk/o;->a:Lnet/gogame/gopay/sdk/StoreActivity;

    invoke-static {p1}, Lnet/gogame/gopay/sdk/StoreActivity;->b(Lnet/gogame/gopay/sdk/StoreActivity;)V

    return-void
.end method

.method public final onNothingSelected(Landroid/widget/AdapterView;)V
    .locals 0

    return-void
.end method
