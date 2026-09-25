.class final Lnet/gogame/gopay/sdk/support/g;
.super Landroid/view/GestureDetector$SimpleOnGestureListener;


# instance fields
.field final synthetic a:Lnet/gogame/gopay/sdk/support/c;


# direct methods
.method private constructor <init>(Lnet/gogame/gopay/sdk/support/c;)V
    .locals 0

    iput-object p1, p0, Lnet/gogame/gopay/sdk/support/g;->a:Lnet/gogame/gopay/sdk/support/c;

    invoke-direct {p0}, Landroid/view/GestureDetector$SimpleOnGestureListener;-><init>()V

    return-void
.end method

.method synthetic constructor <init>(Lnet/gogame/gopay/sdk/support/c;B)V
    .locals 0

    invoke-direct {p0, p1}, Lnet/gogame/gopay/sdk/support/g;-><init>(Lnet/gogame/gopay/sdk/support/c;)V

    return-void
.end method


# virtual methods
.method public final onDown(Landroid/view/MotionEvent;)Z
    .locals 1

    iget-object v0, p0, Lnet/gogame/gopay/sdk/support/g;->a:Lnet/gogame/gopay/sdk/support/c;

    invoke-virtual {v0, p1}, Lnet/gogame/gopay/sdk/support/c;->a(Landroid/view/MotionEvent;)Z

    move-result p1

    return p1
.end method

.method public final onFling(Landroid/view/MotionEvent;Landroid/view/MotionEvent;FF)Z
    .locals 0

    iget-object p1, p0, Lnet/gogame/gopay/sdk/support/g;->a:Lnet/gogame/gopay/sdk/support/c;

    invoke-virtual {p1, p3}, Lnet/gogame/gopay/sdk/support/c;->a(F)Z

    move-result p1

    return p1
.end method

.method public final onLongPress(Landroid/view/MotionEvent;)V
    .locals 7

    iget-object v0, p0, Lnet/gogame/gopay/sdk/support/g;->a:Lnet/gogame/gopay/sdk/support/c;

    invoke-static {v0}, Lnet/gogame/gopay/sdk/support/c;->d(Lnet/gogame/gopay/sdk/support/c;)V

    iget-object v0, p0, Lnet/gogame/gopay/sdk/support/g;->a:Lnet/gogame/gopay/sdk/support/c;

    invoke-virtual {p1}, Landroid/view/MotionEvent;->getX()F

    move-result v1

    float-to-int v1, v1

    invoke-virtual {p1}, Landroid/view/MotionEvent;->getY()F

    move-result p1

    float-to-int p1, p1

    invoke-static {v0, v1, p1}, Lnet/gogame/gopay/sdk/support/c;->a(Lnet/gogame/gopay/sdk/support/c;II)I

    move-result p1

    if-ltz p1, :cond_0

    iget-object v0, p0, Lnet/gogame/gopay/sdk/support/g;->a:Lnet/gogame/gopay/sdk/support/c;

    invoke-static {v0}, Lnet/gogame/gopay/sdk/support/c;->f(Lnet/gogame/gopay/sdk/support/c;)Z

    move-result v0

    if-nez v0, :cond_0

    iget-object v0, p0, Lnet/gogame/gopay/sdk/support/g;->a:Lnet/gogame/gopay/sdk/support/c;

    invoke-virtual {v0, p1}, Lnet/gogame/gopay/sdk/support/c;->getChildAt(I)Landroid/view/View;

    move-result-object v3

    iget-object v0, p0, Lnet/gogame/gopay/sdk/support/g;->a:Lnet/gogame/gopay/sdk/support/c;

    invoke-virtual {v0}, Lnet/gogame/gopay/sdk/support/c;->getOnItemLongClickListener()Landroid/widget/AdapterView$OnItemLongClickListener;

    move-result-object v1

    if-eqz v1, :cond_0

    iget-object v0, p0, Lnet/gogame/gopay/sdk/support/g;->a:Lnet/gogame/gopay/sdk/support/c;

    invoke-static {v0}, Lnet/gogame/gopay/sdk/support/c;->g(Lnet/gogame/gopay/sdk/support/c;)I

    move-result v0

    add-int v4, v0, p1

    iget-object v2, p0, Lnet/gogame/gopay/sdk/support/g;->a:Lnet/gogame/gopay/sdk/support/c;

    iget-object p1, p0, Lnet/gogame/gopay/sdk/support/g;->a:Lnet/gogame/gopay/sdk/support/c;

    iget-object p1, p1, Lnet/gogame/gopay/sdk/support/c;->b:Landroid/widget/ListAdapter;

    invoke-interface {p1, v4}, Landroid/widget/ListAdapter;->getItemId(I)J

    move-result-wide v5

    invoke-interface/range {v1 .. v6}, Landroid/widget/AdapterView$OnItemLongClickListener;->onItemLongClick(Landroid/widget/AdapterView;Landroid/view/View;IJ)Z

    move-result p1

    if-eqz p1, :cond_0

    iget-object p1, p0, Lnet/gogame/gopay/sdk/support/g;->a:Lnet/gogame/gopay/sdk/support/c;

    const/4 v0, 0x0

    invoke-virtual {p1, v0}, Lnet/gogame/gopay/sdk/support/c;->performHapticFeedback(I)Z

    :cond_0
    return-void
.end method

.method public final onScroll(Landroid/view/MotionEvent;Landroid/view/MotionEvent;FF)Z
    .locals 1

    iget-object p1, p0, Lnet/gogame/gopay/sdk/support/g;->a:Lnet/gogame/gopay/sdk/support/c;

    const/4 p2, 0x1

    invoke-static {p2}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object p4

    invoke-static {p1, p4}, Lnet/gogame/gopay/sdk/support/c;->a(Lnet/gogame/gopay/sdk/support/c;Ljava/lang/Boolean;)V

    iget-object p1, p0, Lnet/gogame/gopay/sdk/support/g;->a:Lnet/gogame/gopay/sdk/support/c;

    sget p4, Lnet/gogame/gopay/sdk/support/k;->b:I

    invoke-static {p1, p4}, Lnet/gogame/gopay/sdk/support/c;->a(Lnet/gogame/gopay/sdk/support/c;I)V

    iget-object p1, p0, Lnet/gogame/gopay/sdk/support/g;->a:Lnet/gogame/gopay/sdk/support/c;

    invoke-static {p1}, Lnet/gogame/gopay/sdk/support/c;->d(Lnet/gogame/gopay/sdk/support/c;)V

    iget-object p1, p0, Lnet/gogame/gopay/sdk/support/g;->a:Lnet/gogame/gopay/sdk/support/c;

    iget p4, p1, Lnet/gogame/gopay/sdk/support/c;->d:I

    float-to-int v0, p3

    add-int/2addr p4, v0

    iput p4, p1, Lnet/gogame/gopay/sdk/support/c;->d:I

    iget-object p1, p0, Lnet/gogame/gopay/sdk/support/g;->a:Lnet/gogame/gopay/sdk/support/c;

    invoke-static {p3}, Ljava/lang/Math;->round(F)I

    move-result p3

    invoke-static {p1, p3}, Lnet/gogame/gopay/sdk/support/c;->b(Lnet/gogame/gopay/sdk/support/c;I)V

    iget-object p1, p0, Lnet/gogame/gopay/sdk/support/g;->a:Lnet/gogame/gopay/sdk/support/c;

    invoke-virtual {p1}, Lnet/gogame/gopay/sdk/support/c;->requestLayout()V

    return p2
.end method

.method public final onSingleTapConfirmed(Landroid/view/MotionEvent;)Z
    .locals 7

    iget-object v0, p0, Lnet/gogame/gopay/sdk/support/g;->a:Lnet/gogame/gopay/sdk/support/c;

    invoke-static {v0}, Lnet/gogame/gopay/sdk/support/c;->d(Lnet/gogame/gopay/sdk/support/c;)V

    iget-object v0, p0, Lnet/gogame/gopay/sdk/support/g;->a:Lnet/gogame/gopay/sdk/support/c;

    invoke-virtual {v0}, Lnet/gogame/gopay/sdk/support/c;->getOnItemClickListener()Landroid/widget/AdapterView$OnItemClickListener;

    move-result-object v1

    iget-object v0, p0, Lnet/gogame/gopay/sdk/support/g;->a:Lnet/gogame/gopay/sdk/support/c;

    invoke-virtual {p1}, Landroid/view/MotionEvent;->getX()F

    move-result v2

    float-to-int v2, v2

    invoke-virtual {p1}, Landroid/view/MotionEvent;->getY()F

    move-result p1

    float-to-int p1, p1

    invoke-static {v0, v2, p1}, Lnet/gogame/gopay/sdk/support/c;->a(Lnet/gogame/gopay/sdk/support/c;II)I

    move-result p1

    if-ltz p1, :cond_0

    iget-object v0, p0, Lnet/gogame/gopay/sdk/support/g;->a:Lnet/gogame/gopay/sdk/support/c;

    invoke-static {v0}, Lnet/gogame/gopay/sdk/support/c;->f(Lnet/gogame/gopay/sdk/support/c;)Z

    move-result v0

    if-nez v0, :cond_0

    iget-object v0, p0, Lnet/gogame/gopay/sdk/support/g;->a:Lnet/gogame/gopay/sdk/support/c;

    invoke-virtual {v0, p1}, Lnet/gogame/gopay/sdk/support/c;->getChildAt(I)Landroid/view/View;

    move-result-object v3

    iget-object v0, p0, Lnet/gogame/gopay/sdk/support/g;->a:Lnet/gogame/gopay/sdk/support/c;

    invoke-static {v0}, Lnet/gogame/gopay/sdk/support/c;->g(Lnet/gogame/gopay/sdk/support/c;)I

    move-result v0

    add-int v4, v0, p1

    if-eqz v1, :cond_0

    iget-object v2, p0, Lnet/gogame/gopay/sdk/support/g;->a:Lnet/gogame/gopay/sdk/support/c;

    iget-object p1, p0, Lnet/gogame/gopay/sdk/support/g;->a:Lnet/gogame/gopay/sdk/support/c;

    iget-object p1, p1, Lnet/gogame/gopay/sdk/support/c;->b:Landroid/widget/ListAdapter;

    invoke-interface {p1, v4}, Landroid/widget/ListAdapter;->getItemId(I)J

    move-result-wide v5

    invoke-interface/range {v1 .. v6}, Landroid/widget/AdapterView$OnItemClickListener;->onItemClick(Landroid/widget/AdapterView;Landroid/view/View;IJ)V

    const/4 p1, 0x1

    return p1

    :cond_0
    iget-object p1, p0, Lnet/gogame/gopay/sdk/support/g;->a:Lnet/gogame/gopay/sdk/support/c;

    invoke-static {p1}, Lnet/gogame/gopay/sdk/support/c;->h(Lnet/gogame/gopay/sdk/support/c;)Landroid/view/View$OnClickListener;

    move-result-object p1

    if-eqz p1, :cond_1

    iget-object p1, p0, Lnet/gogame/gopay/sdk/support/g;->a:Lnet/gogame/gopay/sdk/support/c;

    invoke-static {p1}, Lnet/gogame/gopay/sdk/support/c;->f(Lnet/gogame/gopay/sdk/support/c;)Z

    move-result p1

    if-nez p1, :cond_1

    iget-object p1, p0, Lnet/gogame/gopay/sdk/support/g;->a:Lnet/gogame/gopay/sdk/support/c;

    invoke-static {p1}, Lnet/gogame/gopay/sdk/support/c;->h(Lnet/gogame/gopay/sdk/support/c;)Landroid/view/View$OnClickListener;

    move-result-object p1

    iget-object v0, p0, Lnet/gogame/gopay/sdk/support/g;->a:Lnet/gogame/gopay/sdk/support/c;

    invoke-interface {p1, v0}, Landroid/view/View$OnClickListener;->onClick(Landroid/view/View;)V

    :cond_1
    const/4 p1, 0x0

    return p1
.end method
