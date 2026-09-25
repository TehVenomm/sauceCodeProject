.class public final Lnet/gogame/gopay/sdk/support/c;
.super Landroid/widget/AdapterView;


# instance fields
.field private A:Z

.field private B:Z

.field private C:Landroid/view/View$OnClickListener;

.field private D:Z

.field private E:Landroid/database/DataSetObserver;

.field private F:Ljava/lang/Runnable;

.field protected a:Landroid/widget/Scroller;

.field protected b:Landroid/widget/ListAdapter;

.field protected c:I

.field protected d:I

.field private final e:Lnet/gogame/gopay/sdk/support/g;

.field private f:Landroid/view/GestureDetector;

.field private g:I

.field private h:Ljava/util/List;

.field private i:Z

.field private j:Landroid/graphics/Rect;

.field private k:Landroid/view/View;

.field private l:I

.field private m:Landroid/graphics/drawable/Drawable;

.field private n:Ljava/lang/Integer;

.field private o:I

.field private p:I

.field private q:I

.field private r:I

.field private s:Lnet/gogame/gopay/sdk/support/l;

.field private t:I

.field private u:Z

.field private v:Lnet/gogame/gopay/sdk/support/j;

.field private w:I

.field private x:Landroidx/core/widget/EdgeEffectCompat;

.field private y:Landroidx/core/widget/EdgeEffectCompat;

.field private z:I


# direct methods
.method public constructor <init>(Landroid/content/Context;)V
    .locals 3

    const/4 v0, 0x0

    invoke-direct {p0, p1, v0}, Landroid/widget/AdapterView;-><init>(Landroid/content/Context;Landroid/util/AttributeSet;)V

    new-instance v1, Landroid/widget/Scroller;

    invoke-virtual {p0}, Lnet/gogame/gopay/sdk/support/c;->getContext()Landroid/content/Context;

    move-result-object v2

    invoke-direct {v1, v2}, Landroid/widget/Scroller;-><init>(Landroid/content/Context;)V

    iput-object v1, p0, Lnet/gogame/gopay/sdk/support/c;->a:Landroid/widget/Scroller;

    new-instance v1, Lnet/gogame/gopay/sdk/support/g;

    const/4 v2, 0x0

    invoke-direct {v1, p0, v2}, Lnet/gogame/gopay/sdk/support/g;-><init>(Lnet/gogame/gopay/sdk/support/c;B)V

    iput-object v1, p0, Lnet/gogame/gopay/sdk/support/c;->e:Lnet/gogame/gopay/sdk/support/g;

    new-instance v1, Ljava/util/ArrayList;

    invoke-direct {v1}, Ljava/util/ArrayList;-><init>()V

    iput-object v1, p0, Lnet/gogame/gopay/sdk/support/c;->h:Ljava/util/List;

    iput-boolean v2, p0, Lnet/gogame/gopay/sdk/support/c;->i:Z

    new-instance v1, Landroid/graphics/Rect;

    invoke-direct {v1}, Landroid/graphics/Rect;-><init>()V

    iput-object v1, p0, Lnet/gogame/gopay/sdk/support/c;->j:Landroid/graphics/Rect;

    iput-object v0, p0, Lnet/gogame/gopay/sdk/support/c;->k:Landroid/view/View;

    iput v2, p0, Lnet/gogame/gopay/sdk/support/c;->l:I

    iput-object v0, p0, Lnet/gogame/gopay/sdk/support/c;->m:Landroid/graphics/drawable/Drawable;

    iput-object v0, p0, Lnet/gogame/gopay/sdk/support/c;->n:Ljava/lang/Integer;

    const v1, 0x7fffffff

    iput v1, p0, Lnet/gogame/gopay/sdk/support/c;->o:I

    iput-object v0, p0, Lnet/gogame/gopay/sdk/support/c;->s:Lnet/gogame/gopay/sdk/support/l;

    iput v2, p0, Lnet/gogame/gopay/sdk/support/c;->t:I

    iput-boolean v2, p0, Lnet/gogame/gopay/sdk/support/c;->u:Z

    iput-object v0, p0, Lnet/gogame/gopay/sdk/support/c;->v:Lnet/gogame/gopay/sdk/support/j;

    sget v0, Lnet/gogame/gopay/sdk/support/k;->a:I

    iput v0, p0, Lnet/gogame/gopay/sdk/support/c;->w:I

    iput-boolean v2, p0, Lnet/gogame/gopay/sdk/support/c;->A:Z

    iput-boolean v2, p0, Lnet/gogame/gopay/sdk/support/c;->B:Z

    const/4 v0, 0x1

    iput-boolean v0, p0, Lnet/gogame/gopay/sdk/support/c;->D:Z

    new-instance v0, Lnet/gogame/gopay/sdk/support/e;

    invoke-direct {v0, p0}, Lnet/gogame/gopay/sdk/support/e;-><init>(Lnet/gogame/gopay/sdk/support/c;)V

    iput-object v0, p0, Lnet/gogame/gopay/sdk/support/c;->E:Landroid/database/DataSetObserver;

    new-instance v0, Lnet/gogame/gopay/sdk/support/f;

    invoke-direct {v0, p0}, Lnet/gogame/gopay/sdk/support/f;-><init>(Lnet/gogame/gopay/sdk/support/c;)V

    iput-object v0, p0, Lnet/gogame/gopay/sdk/support/c;->F:Ljava/lang/Runnable;

    new-instance v0, Landroidx/core/widget/EdgeEffectCompat;

    invoke-direct {v0, p1}, Landroidx/core/widget/EdgeEffectCompat;-><init>(Landroid/content/Context;)V

    iput-object v0, p0, Lnet/gogame/gopay/sdk/support/c;->x:Landroidx/core/widget/EdgeEffectCompat;

    new-instance v0, Landroidx/core/widget/EdgeEffectCompat;

    invoke-direct {v0, p1}, Landroidx/core/widget/EdgeEffectCompat;-><init>(Landroid/content/Context;)V

    iput-object v0, p0, Lnet/gogame/gopay/sdk/support/c;->y:Landroidx/core/widget/EdgeEffectCompat;

    new-instance v0, Landroid/view/GestureDetector;

    iget-object v1, p0, Lnet/gogame/gopay/sdk/support/c;->e:Lnet/gogame/gopay/sdk/support/g;

    invoke-direct {v0, p1, v1}, Landroid/view/GestureDetector;-><init>(Landroid/content/Context;Landroid/view/GestureDetector$OnGestureListener;)V

    iput-object v0, p0, Lnet/gogame/gopay/sdk/support/c;->f:Landroid/view/GestureDetector;

    new-instance p1, Lnet/gogame/gopay/sdk/support/d;

    invoke-direct {p1, p0}, Lnet/gogame/gopay/sdk/support/d;-><init>(Lnet/gogame/gopay/sdk/support/c;)V

    invoke-virtual {p0, p1}, Lnet/gogame/gopay/sdk/support/c;->setOnTouchListener(Landroid/view/View$OnTouchListener;)V

    invoke-direct {p0}, Lnet/gogame/gopay/sdk/support/c;->a()V

    invoke-virtual {p0, v2}, Lnet/gogame/gopay/sdk/support/c;->setWillNotDraw(Z)V

    sget p1, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 v0, 0xb

    if-lt p1, v0, :cond_0

    iget-object p1, p0, Lnet/gogame/gopay/sdk/support/c;->a:Landroid/widget/Scroller;

    invoke-static {p1}, Lnet/gogame/gopay/sdk/support/h;->a(Landroid/widget/Scroller;)V

    :cond_0
    return-void
.end method

.method private a(II)I
    .locals 4

    invoke-virtual {p0}, Lnet/gogame/gopay/sdk/support/c;->getChildCount()I

    move-result v0

    const/4 v1, 0x0

    :goto_0
    if-ge v1, v0, :cond_1

    invoke-virtual {p0, v1}, Lnet/gogame/gopay/sdk/support/c;->getChildAt(I)Landroid/view/View;

    move-result-object v2

    iget-object v3, p0, Lnet/gogame/gopay/sdk/support/c;->j:Landroid/graphics/Rect;

    invoke-virtual {v2, v3}, Landroid/view/View;->getHitRect(Landroid/graphics/Rect;)V

    iget-object v2, p0, Lnet/gogame/gopay/sdk/support/c;->j:Landroid/graphics/Rect;

    invoke-virtual {v2, p1, p2}, Landroid/graphics/Rect;->contains(II)Z

    move-result v2

    if-eqz v2, :cond_0

    return v1

    :cond_0
    add-int/lit8 v1, v1, 0x1

    goto :goto_0

    :cond_1
    const/4 p1, -0x1

    return p1
.end method

.method static synthetic a(Lnet/gogame/gopay/sdk/support/c;II)I
    .locals 0

    invoke-direct {p0, p1, p2}, Lnet/gogame/gopay/sdk/support/c;->a(II)I

    move-result p0

    return p0
.end method

.method static synthetic a(Lnet/gogame/gopay/sdk/support/c;)Landroid/view/GestureDetector;
    .locals 0

    iget-object p0, p0, Lnet/gogame/gopay/sdk/support/c;->f:Landroid/view/GestureDetector;

    return-object p0
.end method

.method private a(I)Landroid/view/View;
    .locals 1

    iget-object v0, p0, Lnet/gogame/gopay/sdk/support/c;->b:Landroid/widget/ListAdapter;

    invoke-interface {v0, p1}, Landroid/widget/ListAdapter;->getItemViewType(I)I

    move-result p1

    invoke-direct {p0, p1}, Lnet/gogame/gopay/sdk/support/c;->b(I)Z

    move-result v0

    if-eqz v0, :cond_0

    iget-object v0, p0, Lnet/gogame/gopay/sdk/support/c;->h:Ljava/util/List;

    invoke-interface {v0, p1}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Ljava/util/Queue;

    invoke-interface {p1}, Ljava/util/Queue;->poll()Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Landroid/view/View;

    return-object p1

    :cond_0
    const/4 p1, 0x0

    return-object p1
.end method

.method private static a(Landroid/view/View;)Landroid/view/ViewGroup$LayoutParams;
    .locals 2

    invoke-virtual {p0}, Landroid/view/View;->getLayoutParams()Landroid/view/ViewGroup$LayoutParams;

    move-result-object p0

    if-nez p0, :cond_0

    new-instance p0, Landroid/view/ViewGroup$LayoutParams;

    const/4 v0, -0x2

    const/4 v1, -0x1

    invoke-direct {p0, v0, v1}, Landroid/view/ViewGroup$LayoutParams;-><init>(II)V

    :cond_0
    return-object p0
.end method

.method private a()V
    .locals 1

    const/4 v0, -0x1

    iput v0, p0, Lnet/gogame/gopay/sdk/support/c;->p:I

    iput v0, p0, Lnet/gogame/gopay/sdk/support/c;->q:I

    const/4 v0, 0x0

    iput v0, p0, Lnet/gogame/gopay/sdk/support/c;->g:I

    iput v0, p0, Lnet/gogame/gopay/sdk/support/c;->c:I

    iput v0, p0, Lnet/gogame/gopay/sdk/support/c;->d:I

    const v0, 0x7fffffff

    iput v0, p0, Lnet/gogame/gopay/sdk/support/c;->o:I

    sget v0, Lnet/gogame/gopay/sdk/support/k;->a:I

    invoke-direct {p0, v0}, Lnet/gogame/gopay/sdk/support/c;->setCurrentScrollState$6c40596b(I)V

    return-void
.end method

.method private a(ILandroid/view/View;)V
    .locals 1

    iget-object v0, p0, Lnet/gogame/gopay/sdk/support/c;->b:Landroid/widget/ListAdapter;

    invoke-interface {v0, p1}, Landroid/widget/ListAdapter;->getItemViewType(I)I

    move-result p1

    invoke-direct {p0, p1}, Lnet/gogame/gopay/sdk/support/c;->b(I)Z

    move-result v0

    if-eqz v0, :cond_0

    iget-object v0, p0, Lnet/gogame/gopay/sdk/support/c;->h:Ljava/util/List;

    invoke-interface {v0, p1}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Ljava/util/Queue;

    invoke-interface {p1, p2}, Ljava/util/Queue;->offer(Ljava/lang/Object;)Z

    :cond_0
    return-void
.end method

.method private a(Landroid/graphics/Canvas;Landroid/graphics/Rect;)V
    .locals 1

    iget-object v0, p0, Lnet/gogame/gopay/sdk/support/c;->m:Landroid/graphics/drawable/Drawable;

    if-eqz v0, :cond_0

    iget-object v0, p0, Lnet/gogame/gopay/sdk/support/c;->m:Landroid/graphics/drawable/Drawable;

    invoke-virtual {v0, p2}, Landroid/graphics/drawable/Drawable;->setBounds(Landroid/graphics/Rect;)V

    iget-object p2, p0, Lnet/gogame/gopay/sdk/support/c;->m:Landroid/graphics/drawable/Drawable;

    invoke-virtual {p2, p1}, Landroid/graphics/drawable/Drawable;->draw(Landroid/graphics/Canvas;)V

    :cond_0
    return-void
.end method

.method private a(Landroid/view/View;I)V
    .locals 3

    invoke-static {p1}, Lnet/gogame/gopay/sdk/support/c;->a(Landroid/view/View;)Landroid/view/ViewGroup$LayoutParams;

    move-result-object v0

    const/4 v1, 0x1

    invoke-virtual {p0, p1, p2, v0, v1}, Lnet/gogame/gopay/sdk/support/c;->addViewInLayout(Landroid/view/View;ILandroid/view/ViewGroup$LayoutParams;Z)Z

    invoke-static {p1}, Lnet/gogame/gopay/sdk/support/c;->a(Landroid/view/View;)Landroid/view/ViewGroup$LayoutParams;

    move-result-object p2

    iget v0, p0, Lnet/gogame/gopay/sdk/support/c;->z:I

    invoke-virtual {p0}, Lnet/gogame/gopay/sdk/support/c;->getPaddingTop()I

    move-result v1

    invoke-virtual {p0}, Lnet/gogame/gopay/sdk/support/c;->getPaddingBottom()I

    move-result v2

    add-int/2addr v1, v2

    iget v2, p2, Landroid/view/ViewGroup$LayoutParams;->height:I

    invoke-static {v0, v1, v2}, Landroid/view/ViewGroup;->getChildMeasureSpec(III)I

    move-result v0

    iget v1, p2, Landroid/view/ViewGroup$LayoutParams;->width:I

    if-lez v1, :cond_0

    iget p2, p2, Landroid/view/ViewGroup$LayoutParams;->width:I

    const/high16 v1, 0x40000000    # 2.0f

    invoke-static {p2, v1}, Landroid/view/View$MeasureSpec;->makeMeasureSpec(II)I

    move-result p2

    goto :goto_0

    :cond_0
    const/4 p2, 0x0

    invoke-static {p2, p2}, Landroid/view/View$MeasureSpec;->makeMeasureSpec(II)I

    move-result p2

    :goto_0
    invoke-virtual {p1, p2, v0}, Landroid/view/View;->measure(II)V

    return-void
.end method

.method private a(Ljava/lang/Boolean;)V
    .locals 2

    iget-boolean v0, p0, Lnet/gogame/gopay/sdk/support/c;->B:Z

    invoke-virtual {p1}, Ljava/lang/Boolean;->booleanValue()Z

    move-result v1

    if-eq v0, v1, :cond_2

    move-object v0, p0

    :goto_0
    invoke-virtual {v0}, Landroid/view/View;->getParent()Landroid/view/ViewParent;

    move-result-object v1

    instance-of v1, v1, Landroid/view/View;

    if-eqz v1, :cond_2

    invoke-virtual {v0}, Landroid/view/View;->getParent()Landroid/view/ViewParent;

    move-result-object v1

    instance-of v1, v1, Landroid/widget/ListView;

    if-nez v1, :cond_1

    invoke-virtual {v0}, Landroid/view/View;->getParent()Landroid/view/ViewParent;

    move-result-object v1

    instance-of v1, v1, Landroid/widget/ScrollView;

    if-eqz v1, :cond_0

    goto :goto_1

    :cond_0
    invoke-virtual {v0}, Landroid/view/View;->getParent()Landroid/view/ViewParent;

    move-result-object v0

    check-cast v0, Landroid/view/View;

    goto :goto_0

    :cond_1
    :goto_1
    invoke-virtual {v0}, Landroid/view/View;->getParent()Landroid/view/ViewParent;

    move-result-object v0

    invoke-virtual {p1}, Ljava/lang/Boolean;->booleanValue()Z

    move-result v1

    invoke-interface {v0, v1}, Landroid/view/ViewParent;->requestDisallowInterceptTouchEvent(Z)V

    invoke-virtual {p1}, Ljava/lang/Boolean;->booleanValue()Z

    move-result p1

    iput-boolean p1, p0, Lnet/gogame/gopay/sdk/support/c;->B:Z

    :cond_2
    return-void
.end method

.method static synthetic a(Lnet/gogame/gopay/sdk/support/c;I)V
    .locals 0

    invoke-direct {p0, p1}, Lnet/gogame/gopay/sdk/support/c;->setCurrentScrollState$6c40596b(I)V

    return-void
.end method

.method static synthetic a(Lnet/gogame/gopay/sdk/support/c;Ljava/lang/Boolean;)V
    .locals 0

    invoke-direct {p0, p1}, Lnet/gogame/gopay/sdk/support/c;->a(Ljava/lang/Boolean;)V

    return-void
.end method

.method private b()V
    .locals 0

    invoke-direct {p0}, Lnet/gogame/gopay/sdk/support/c;->a()V

    invoke-virtual {p0}, Lnet/gogame/gopay/sdk/support/c;->removeAllViewsInLayout()V

    invoke-virtual {p0}, Lnet/gogame/gopay/sdk/support/c;->requestLayout()V

    return-void
.end method

.method static synthetic b(Lnet/gogame/gopay/sdk/support/c;I)V
    .locals 2

    iget-object v0, p0, Lnet/gogame/gopay/sdk/support/c;->x:Landroidx/core/widget/EdgeEffectCompat;

    if-eqz v0, :cond_4

    iget-object v0, p0, Lnet/gogame/gopay/sdk/support/c;->y:Landroidx/core/widget/EdgeEffectCompat;

    if-nez v0, :cond_0

    goto :goto_0

    :cond_0
    iget v0, p0, Lnet/gogame/gopay/sdk/support/c;->c:I

    add-int/2addr v0, p1

    iget-object v1, p0, Lnet/gogame/gopay/sdk/support/c;->a:Landroid/widget/Scroller;

    if-eqz v1, :cond_1

    iget-object v1, p0, Lnet/gogame/gopay/sdk/support/c;->a:Landroid/widget/Scroller;

    invoke-virtual {v1}, Landroid/widget/Scroller;->isFinished()Z

    move-result v1

    if-eqz v1, :cond_4

    :cond_1
    if-gez v0, :cond_3

    invoke-static {p1}, Ljava/lang/Math;->abs(I)I

    move-result p1

    iget-object v0, p0, Lnet/gogame/gopay/sdk/support/c;->x:Landroidx/core/widget/EdgeEffectCompat;

    int-to-float p1, p1

    invoke-direct {p0}, Lnet/gogame/gopay/sdk/support/c;->getRenderWidth()I

    move-result v1

    int-to-float v1, v1

    div-float/2addr p1, v1

    invoke-virtual {v0, p1}, Landroidx/core/widget/EdgeEffectCompat;->onPull(F)Z

    iget-object p1, p0, Lnet/gogame/gopay/sdk/support/c;->y:Landroidx/core/widget/EdgeEffectCompat;

    invoke-virtual {p1}, Landroidx/core/widget/EdgeEffectCompat;->isFinished()Z

    move-result p1

    if-nez p1, :cond_2

    iget-object p0, p0, Lnet/gogame/gopay/sdk/support/c;->y:Landroidx/core/widget/EdgeEffectCompat;

    invoke-virtual {p0}, Landroidx/core/widget/EdgeEffectCompat;->onRelease()Z

    :cond_2
    return-void

    :cond_3
    iget v1, p0, Lnet/gogame/gopay/sdk/support/c;->o:I

    if-le v0, v1, :cond_4

    invoke-static {p1}, Ljava/lang/Math;->abs(I)I

    move-result p1

    iget-object v0, p0, Lnet/gogame/gopay/sdk/support/c;->y:Landroidx/core/widget/EdgeEffectCompat;

    int-to-float p1, p1

    invoke-direct {p0}, Lnet/gogame/gopay/sdk/support/c;->getRenderWidth()I

    move-result v1

    int-to-float v1, v1

    div-float/2addr p1, v1

    invoke-virtual {v0, p1}, Landroidx/core/widget/EdgeEffectCompat;->onPull(F)Z

    iget-object p1, p0, Lnet/gogame/gopay/sdk/support/c;->x:Landroidx/core/widget/EdgeEffectCompat;

    invoke-virtual {p1}, Landroidx/core/widget/EdgeEffectCompat;->isFinished()Z

    move-result p1

    if-nez p1, :cond_4

    iget-object p0, p0, Lnet/gogame/gopay/sdk/support/c;->x:Landroidx/core/widget/EdgeEffectCompat;

    invoke-virtual {p0}, Landroidx/core/widget/EdgeEffectCompat;->onRelease()Z

    :cond_4
    :goto_0
    return-void
.end method

.method private b(I)Z
    .locals 1

    iget-object v0, p0, Lnet/gogame/gopay/sdk/support/c;->h:Ljava/util/List;

    invoke-interface {v0}, Ljava/util/List;->size()I

    move-result v0

    if-ge p1, v0, :cond_0

    const/4 p1, 0x1

    return p1

    :cond_0
    const/4 p1, 0x0

    return p1
.end method

.method static synthetic b(Lnet/gogame/gopay/sdk/support/c;)Z
    .locals 1

    const/4 v0, 0x1

    iput-boolean v0, p0, Lnet/gogame/gopay/sdk/support/c;->i:Z

    return v0
.end method

.method private c()F
    .locals 2

    sget v0, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 v1, 0xe

    if-lt v0, v1, :cond_0

    iget-object v0, p0, Lnet/gogame/gopay/sdk/support/c;->a:Landroid/widget/Scroller;

    invoke-static {v0}, Lnet/gogame/gopay/sdk/support/i;->a(Landroid/widget/Scroller;)F

    move-result v0

    return v0

    :cond_0
    const/high16 v0, 0x41f00000    # 30.0f

    return v0
.end method

.method private c(I)Z
    .locals 2

    iget-object v0, p0, Lnet/gogame/gopay/sdk/support/c;->b:Landroid/widget/ListAdapter;

    invoke-interface {v0}, Landroid/widget/ListAdapter;->getCount()I

    move-result v0

    const/4 v1, 0x1

    sub-int/2addr v0, v1

    if-ne p1, v0, :cond_0

    return v1

    :cond_0
    const/4 p1, 0x0

    return p1
.end method

.method static synthetic c(Lnet/gogame/gopay/sdk/support/c;)Z
    .locals 1

    const/4 v0, 0x0

    iput-boolean v0, p0, Lnet/gogame/gopay/sdk/support/c;->u:Z

    return v0
.end method

.method private d()V
    .locals 2

    iget-object v0, p0, Lnet/gogame/gopay/sdk/support/c;->k:Landroid/view/View;

    if-eqz v0, :cond_0

    iget-object v0, p0, Lnet/gogame/gopay/sdk/support/c;->k:Landroid/view/View;

    const/4 v1, 0x0

    invoke-virtual {v0, v1}, Landroid/view/View;->setPressed(Z)V

    invoke-virtual {p0}, Lnet/gogame/gopay/sdk/support/c;->refreshDrawableState()V

    const/4 v0, 0x0

    iput-object v0, p0, Lnet/gogame/gopay/sdk/support/c;->k:Landroid/view/View;

    :cond_0
    return-void
.end method

.method static synthetic d(Lnet/gogame/gopay/sdk/support/c;)V
    .locals 0

    invoke-direct {p0}, Lnet/gogame/gopay/sdk/support/c;->d()V

    return-void
.end method

.method private e()V
    .locals 1

    iget-object v0, p0, Lnet/gogame/gopay/sdk/support/c;->x:Landroidx/core/widget/EdgeEffectCompat;

    if-eqz v0, :cond_0

    iget-object v0, p0, Lnet/gogame/gopay/sdk/support/c;->x:Landroidx/core/widget/EdgeEffectCompat;

    invoke-virtual {v0}, Landroidx/core/widget/EdgeEffectCompat;->onRelease()Z

    :cond_0
    iget-object v0, p0, Lnet/gogame/gopay/sdk/support/c;->y:Landroidx/core/widget/EdgeEffectCompat;

    if-eqz v0, :cond_1

    iget-object v0, p0, Lnet/gogame/gopay/sdk/support/c;->y:Landroidx/core/widget/EdgeEffectCompat;

    invoke-virtual {v0}, Landroidx/core/widget/EdgeEffectCompat;->onRelease()Z

    :cond_1
    return-void
.end method

.method static synthetic e(Lnet/gogame/gopay/sdk/support/c;)V
    .locals 0

    invoke-direct {p0}, Lnet/gogame/gopay/sdk/support/c;->b()V

    return-void
.end method

.method private f()Z
    .locals 2

    iget-object v0, p0, Lnet/gogame/gopay/sdk/support/c;->b:Landroid/widget/ListAdapter;

    const/4 v1, 0x0

    if-eqz v0, :cond_1

    iget-object v0, p0, Lnet/gogame/gopay/sdk/support/c;->b:Landroid/widget/ListAdapter;

    invoke-interface {v0}, Landroid/widget/ListAdapter;->isEmpty()Z

    move-result v0

    if-eqz v0, :cond_0

    goto :goto_0

    :cond_0
    iget v0, p0, Lnet/gogame/gopay/sdk/support/c;->o:I

    if-lez v0, :cond_1

    const/4 v0, 0x1

    return v0

    :cond_1
    :goto_0
    return v1
.end method

.method static synthetic f(Lnet/gogame/gopay/sdk/support/c;)Z
    .locals 0

    iget-boolean p0, p0, Lnet/gogame/gopay/sdk/support/c;->A:Z

    return p0
.end method

.method static synthetic g(Lnet/gogame/gopay/sdk/support/c;)I
    .locals 0

    iget p0, p0, Lnet/gogame/gopay/sdk/support/c;->p:I

    return p0
.end method

.method private getLeftmostChild()Landroid/view/View;
    .locals 1

    const/4 v0, 0x0

    invoke-virtual {p0, v0}, Lnet/gogame/gopay/sdk/support/c;->getChildAt(I)Landroid/view/View;

    move-result-object v0

    return-object v0
.end method

.method private getRenderHeight()I
    .locals 2

    invoke-virtual {p0}, Lnet/gogame/gopay/sdk/support/c;->getHeight()I

    move-result v0

    invoke-virtual {p0}, Lnet/gogame/gopay/sdk/support/c;->getPaddingTop()I

    move-result v1

    sub-int/2addr v0, v1

    invoke-virtual {p0}, Lnet/gogame/gopay/sdk/support/c;->getPaddingBottom()I

    move-result v1

    sub-int/2addr v0, v1

    return v0
.end method

.method private getRenderWidth()I
    .locals 2

    invoke-virtual {p0}, Lnet/gogame/gopay/sdk/support/c;->getWidth()I

    move-result v0

    invoke-virtual {p0}, Lnet/gogame/gopay/sdk/support/c;->getPaddingLeft()I

    move-result v1

    sub-int/2addr v0, v1

    invoke-virtual {p0}, Lnet/gogame/gopay/sdk/support/c;->getPaddingRight()I

    move-result v1

    sub-int/2addr v0, v1

    return v0
.end method

.method private getRightmostChild()Landroid/view/View;
    .locals 1

    invoke-virtual {p0}, Lnet/gogame/gopay/sdk/support/c;->getChildCount()I

    move-result v0

    add-int/lit8 v0, v0, -0x1

    invoke-virtual {p0, v0}, Lnet/gogame/gopay/sdk/support/c;->getChildAt(I)Landroid/view/View;

    move-result-object v0

    return-object v0
.end method

.method static synthetic h(Lnet/gogame/gopay/sdk/support/c;)Landroid/view/View$OnClickListener;
    .locals 0

    iget-object p0, p0, Lnet/gogame/gopay/sdk/support/c;->C:Landroid/view/View$OnClickListener;

    return-object p0
.end method

.method private setCurrentScrollState$6c40596b(I)V
    .locals 0

    iput p1, p0, Lnet/gogame/gopay/sdk/support/c;->w:I

    return-void
.end method


# virtual methods
.method protected final a(F)Z
    .locals 9

    iget-object v0, p0, Lnet/gogame/gopay/sdk/support/c;->a:Landroid/widget/Scroller;

    iget v1, p0, Lnet/gogame/gopay/sdk/support/c;->d:I

    neg-float p1, p1

    float-to-int v3, p1

    iget v6, p0, Lnet/gogame/gopay/sdk/support/c;->o:I

    const/4 v2, 0x0

    const/4 v4, 0x0

    const/4 v5, 0x0

    const/4 v7, 0x0

    const/4 v8, 0x0

    invoke-virtual/range {v0 .. v8}, Landroid/widget/Scroller;->fling(IIIIIIII)V

    sget p1, Lnet/gogame/gopay/sdk/support/k;->c:I

    invoke-direct {p0, p1}, Lnet/gogame/gopay/sdk/support/c;->setCurrentScrollState$6c40596b(I)V

    invoke-virtual {p0}, Lnet/gogame/gopay/sdk/support/c;->requestLayout()V

    const/4 p1, 0x1

    return p1
.end method

.method protected final a(Landroid/view/MotionEvent;)Z
    .locals 2

    iget-object v0, p0, Lnet/gogame/gopay/sdk/support/c;->a:Landroid/widget/Scroller;

    invoke-virtual {v0}, Landroid/widget/Scroller;->isFinished()Z

    move-result v0

    const/4 v1, 0x1

    xor-int/2addr v0, v1

    iput-boolean v0, p0, Lnet/gogame/gopay/sdk/support/c;->A:Z

    iget-object v0, p0, Lnet/gogame/gopay/sdk/support/c;->a:Landroid/widget/Scroller;

    invoke-virtual {v0, v1}, Landroid/widget/Scroller;->forceFinished(Z)V

    sget v0, Lnet/gogame/gopay/sdk/support/k;->a:I

    invoke-direct {p0, v0}, Lnet/gogame/gopay/sdk/support/c;->setCurrentScrollState$6c40596b(I)V

    invoke-direct {p0}, Lnet/gogame/gopay/sdk/support/c;->d()V

    iget-boolean v0, p0, Lnet/gogame/gopay/sdk/support/c;->A:Z

    if-nez v0, :cond_0

    invoke-virtual {p1}, Landroid/view/MotionEvent;->getX()F

    move-result v0

    float-to-int v0, v0

    invoke-virtual {p1}, Landroid/view/MotionEvent;->getY()F

    move-result p1

    float-to-int p1, p1

    invoke-direct {p0, v0, p1}, Lnet/gogame/gopay/sdk/support/c;->a(II)I

    move-result p1

    if-ltz p1, :cond_0

    invoke-virtual {p0, p1}, Lnet/gogame/gopay/sdk/support/c;->getChildAt(I)Landroid/view/View;

    move-result-object p1

    iput-object p1, p0, Lnet/gogame/gopay/sdk/support/c;->k:Landroid/view/View;

    iget-object p1, p0, Lnet/gogame/gopay/sdk/support/c;->k:Landroid/view/View;

    if-eqz p1, :cond_0

    iget-object p1, p0, Lnet/gogame/gopay/sdk/support/c;->k:Landroid/view/View;

    invoke-virtual {p1, v1}, Landroid/view/View;->setPressed(Z)V

    invoke-virtual {p0}, Lnet/gogame/gopay/sdk/support/c;->refreshDrawableState()V

    :cond_0
    return v1
.end method

.method protected final dispatchDraw(Landroid/graphics/Canvas;)V
    .locals 4

    invoke-super {p0, p1}, Landroid/widget/AdapterView;->dispatchDraw(Landroid/graphics/Canvas;)V

    iget-object v0, p0, Lnet/gogame/gopay/sdk/support/c;->x:Landroidx/core/widget/EdgeEffectCompat;

    const/4 v1, 0x0

    if-eqz v0, :cond_1

    iget-object v0, p0, Lnet/gogame/gopay/sdk/support/c;->x:Landroidx/core/widget/EdgeEffectCompat;

    invoke-virtual {v0}, Landroidx/core/widget/EdgeEffectCompat;->isFinished()Z

    move-result v0

    if-nez v0, :cond_1

    invoke-direct {p0}, Lnet/gogame/gopay/sdk/support/c;->f()Z

    move-result v0

    if-eqz v0, :cond_1

    invoke-virtual {p1}, Landroid/graphics/Canvas;->save()I

    move-result v0

    invoke-virtual {p0}, Lnet/gogame/gopay/sdk/support/c;->getHeight()I

    move-result v2

    const/high16 v3, -0x3d4c0000    # -90.0f

    invoke-virtual {p1, v3, v1, v1}, Landroid/graphics/Canvas;->rotate(FFF)V

    neg-int v2, v2

    invoke-virtual {p0}, Lnet/gogame/gopay/sdk/support/c;->getPaddingBottom()I

    move-result v3

    add-int/2addr v2, v3

    int-to-float v2, v2

    invoke-virtual {p1, v2, v1}, Landroid/graphics/Canvas;->translate(FF)V

    iget-object v1, p0, Lnet/gogame/gopay/sdk/support/c;->x:Landroidx/core/widget/EdgeEffectCompat;

    invoke-direct {p0}, Lnet/gogame/gopay/sdk/support/c;->getRenderHeight()I

    move-result v2

    invoke-direct {p0}, Lnet/gogame/gopay/sdk/support/c;->getRenderWidth()I

    move-result v3

    invoke-virtual {v1, v2, v3}, Landroidx/core/widget/EdgeEffectCompat;->setSize(II)V

    iget-object v1, p0, Lnet/gogame/gopay/sdk/support/c;->x:Landroidx/core/widget/EdgeEffectCompat;

    invoke-virtual {v1, p1}, Landroidx/core/widget/EdgeEffectCompat;->draw(Landroid/graphics/Canvas;)Z

    move-result v1

    if-eqz v1, :cond_0

    invoke-virtual {p0}, Lnet/gogame/gopay/sdk/support/c;->invalidate()V

    :cond_0
    invoke-virtual {p1, v0}, Landroid/graphics/Canvas;->restoreToCount(I)V

    return-void

    :cond_1
    iget-object v0, p0, Lnet/gogame/gopay/sdk/support/c;->y:Landroidx/core/widget/EdgeEffectCompat;

    if-eqz v0, :cond_3

    iget-object v0, p0, Lnet/gogame/gopay/sdk/support/c;->y:Landroidx/core/widget/EdgeEffectCompat;

    invoke-virtual {v0}, Landroidx/core/widget/EdgeEffectCompat;->isFinished()Z

    move-result v0

    if-nez v0, :cond_3

    invoke-direct {p0}, Lnet/gogame/gopay/sdk/support/c;->f()Z

    move-result v0

    if-eqz v0, :cond_3

    invoke-virtual {p1}, Landroid/graphics/Canvas;->save()I

    move-result v0

    invoke-virtual {p0}, Lnet/gogame/gopay/sdk/support/c;->getWidth()I

    move-result v2

    const/high16 v3, 0x42b40000    # 90.0f

    invoke-virtual {p1, v3, v1, v1}, Landroid/graphics/Canvas;->rotate(FFF)V

    invoke-virtual {p0}, Lnet/gogame/gopay/sdk/support/c;->getPaddingTop()I

    move-result v1

    int-to-float v1, v1

    neg-int v2, v2

    int-to-float v2, v2

    invoke-virtual {p1, v1, v2}, Landroid/graphics/Canvas;->translate(FF)V

    iget-object v1, p0, Lnet/gogame/gopay/sdk/support/c;->y:Landroidx/core/widget/EdgeEffectCompat;

    invoke-direct {p0}, Lnet/gogame/gopay/sdk/support/c;->getRenderHeight()I

    move-result v2

    invoke-direct {p0}, Lnet/gogame/gopay/sdk/support/c;->getRenderWidth()I

    move-result v3

    invoke-virtual {v1, v2, v3}, Landroidx/core/widget/EdgeEffectCompat;->setSize(II)V

    iget-object v1, p0, Lnet/gogame/gopay/sdk/support/c;->y:Landroidx/core/widget/EdgeEffectCompat;

    invoke-virtual {v1, p1}, Landroidx/core/widget/EdgeEffectCompat;->draw(Landroid/graphics/Canvas;)Z

    move-result v1

    if-eqz v1, :cond_2

    invoke-virtual {p0}, Lnet/gogame/gopay/sdk/support/c;->invalidate()V

    :cond_2
    invoke-virtual {p1, v0}, Landroid/graphics/Canvas;->restoreToCount(I)V

    :cond_3
    return-void
.end method

.method protected final dispatchSetPressed(Z)V
    .locals 0

    return-void
.end method

.method public final bridge synthetic getAdapter()Landroid/widget/Adapter;
    .locals 1

    invoke-virtual {p0}, Lnet/gogame/gopay/sdk/support/c;->getAdapter()Landroid/widget/ListAdapter;

    move-result-object v0

    return-object v0
.end method

.method public final getAdapter()Landroid/widget/ListAdapter;
    .locals 1

    iget-object v0, p0, Lnet/gogame/gopay/sdk/support/c;->b:Landroid/widget/ListAdapter;

    return-object v0
.end method

.method public final getFirstVisiblePosition()I
    .locals 1

    iget v0, p0, Lnet/gogame/gopay/sdk/support/c;->p:I

    return v0
.end method

.method public final getLastVisiblePosition()I
    .locals 1

    iget v0, p0, Lnet/gogame/gopay/sdk/support/c;->q:I

    return v0
.end method

.method protected final getLeftFadingEdgeStrength()F
    .locals 2

    invoke-virtual {p0}, Lnet/gogame/gopay/sdk/support/c;->getHorizontalFadingEdgeLength()I

    move-result v0

    iget v1, p0, Lnet/gogame/gopay/sdk/support/c;->c:I

    if-nez v1, :cond_0

    const/4 v0, 0x0

    return v0

    :cond_0
    iget v1, p0, Lnet/gogame/gopay/sdk/support/c;->c:I

    if-ge v1, v0, :cond_1

    iget v1, p0, Lnet/gogame/gopay/sdk/support/c;->c:I

    int-to-float v1, v1

    int-to-float v0, v0

    div-float/2addr v1, v0

    return v1

    :cond_1
    const/high16 v0, 0x3f800000    # 1.0f

    return v0
.end method

.method protected final getRightFadingEdgeStrength()F
    .locals 3

    invoke-virtual {p0}, Lnet/gogame/gopay/sdk/support/c;->getHorizontalFadingEdgeLength()I

    move-result v0

    iget v1, p0, Lnet/gogame/gopay/sdk/support/c;->c:I

    iget v2, p0, Lnet/gogame/gopay/sdk/support/c;->o:I

    if-ne v1, v2, :cond_0

    const/4 v0, 0x0

    return v0

    :cond_0
    iget v1, p0, Lnet/gogame/gopay/sdk/support/c;->o:I

    iget v2, p0, Lnet/gogame/gopay/sdk/support/c;->c:I

    sub-int/2addr v1, v2

    if-ge v1, v0, :cond_1

    iget v1, p0, Lnet/gogame/gopay/sdk/support/c;->o:I

    iget v2, p0, Lnet/gogame/gopay/sdk/support/c;->c:I

    sub-int/2addr v1, v2

    int-to-float v1, v1

    int-to-float v0, v0

    div-float/2addr v1, v0

    return v1

    :cond_1
    const/high16 v0, 0x3f800000    # 1.0f

    return v0
.end method

.method public final getSelectedView()Landroid/view/View;
    .locals 2

    iget v0, p0, Lnet/gogame/gopay/sdk/support/c;->r:I

    iget v1, p0, Lnet/gogame/gopay/sdk/support/c;->p:I

    if-lt v0, v1, :cond_0

    iget v1, p0, Lnet/gogame/gopay/sdk/support/c;->q:I

    if-gt v0, v1, :cond_0

    iget v1, p0, Lnet/gogame/gopay/sdk/support/c;->p:I

    sub-int/2addr v0, v1

    invoke-virtual {p0, v0}, Lnet/gogame/gopay/sdk/support/c;->getChildAt(I)Landroid/view/View;

    move-result-object v0

    return-object v0

    :cond_0
    const/4 v0, 0x0

    return-object v0
.end method

.method protected final onDraw(Landroid/graphics/Canvas;)V
    .locals 7

    invoke-super {p0, p1}, Landroid/widget/AdapterView;->onDraw(Landroid/graphics/Canvas;)V

    invoke-virtual {p0}, Lnet/gogame/gopay/sdk/support/c;->getChildCount()I

    move-result v0

    iget-object v1, p0, Lnet/gogame/gopay/sdk/support/c;->j:Landroid/graphics/Rect;

    iget-object v2, p0, Lnet/gogame/gopay/sdk/support/c;->j:Landroid/graphics/Rect;

    invoke-virtual {p0}, Lnet/gogame/gopay/sdk/support/c;->getPaddingTop()I

    move-result v3

    iput v3, v2, Landroid/graphics/Rect;->top:I

    iget-object v2, p0, Lnet/gogame/gopay/sdk/support/c;->j:Landroid/graphics/Rect;

    iget-object v3, p0, Lnet/gogame/gopay/sdk/support/c;->j:Landroid/graphics/Rect;

    iget v3, v3, Landroid/graphics/Rect;->top:I

    invoke-direct {p0}, Lnet/gogame/gopay/sdk/support/c;->getRenderHeight()I

    move-result v4

    add-int/2addr v3, v4

    iput v3, v2, Landroid/graphics/Rect;->bottom:I

    const/4 v2, 0x0

    :goto_0
    if-ge v2, v0, :cond_4

    add-int/lit8 v3, v0, -0x1

    if-ne v2, v3, :cond_0

    iget v3, p0, Lnet/gogame/gopay/sdk/support/c;->q:I

    invoke-direct {p0, v3}, Lnet/gogame/gopay/sdk/support/c;->c(I)Z

    move-result v3

    if-nez v3, :cond_3

    :cond_0
    invoke-virtual {p0, v2}, Lnet/gogame/gopay/sdk/support/c;->getChildAt(I)Landroid/view/View;

    move-result-object v3

    invoke-virtual {v3}, Landroid/view/View;->getRight()I

    move-result v4

    iput v4, v1, Landroid/graphics/Rect;->left:I

    invoke-virtual {v3}, Landroid/view/View;->getRight()I

    move-result v4

    iget v5, p0, Lnet/gogame/gopay/sdk/support/c;->l:I

    add-int/2addr v4, v5

    iput v4, v1, Landroid/graphics/Rect;->right:I

    iget v4, v1, Landroid/graphics/Rect;->left:I

    invoke-virtual {p0}, Lnet/gogame/gopay/sdk/support/c;->getPaddingLeft()I

    move-result v5

    if-ge v4, v5, :cond_1

    invoke-virtual {p0}, Lnet/gogame/gopay/sdk/support/c;->getPaddingLeft()I

    move-result v4

    iput v4, v1, Landroid/graphics/Rect;->left:I

    :cond_1
    iget v4, v1, Landroid/graphics/Rect;->right:I

    invoke-virtual {p0}, Lnet/gogame/gopay/sdk/support/c;->getWidth()I

    move-result v5

    invoke-virtual {p0}, Lnet/gogame/gopay/sdk/support/c;->getPaddingRight()I

    move-result v6

    sub-int/2addr v5, v6

    if-le v4, v5, :cond_2

    invoke-virtual {p0}, Lnet/gogame/gopay/sdk/support/c;->getWidth()I

    move-result v4

    invoke-virtual {p0}, Lnet/gogame/gopay/sdk/support/c;->getPaddingRight()I

    move-result v5

    sub-int/2addr v4, v5

    iput v4, v1, Landroid/graphics/Rect;->right:I

    :cond_2
    invoke-direct {p0, p1, v1}, Lnet/gogame/gopay/sdk/support/c;->a(Landroid/graphics/Canvas;Landroid/graphics/Rect;)V

    if-nez v2, :cond_3

    invoke-virtual {v3}, Landroid/view/View;->getLeft()I

    move-result v4

    invoke-virtual {p0}, Lnet/gogame/gopay/sdk/support/c;->getPaddingLeft()I

    move-result v5

    if-le v4, v5, :cond_3

    invoke-virtual {p0}, Lnet/gogame/gopay/sdk/support/c;->getPaddingLeft()I

    move-result v4

    iput v4, v1, Landroid/graphics/Rect;->left:I

    invoke-virtual {v3}, Landroid/view/View;->getLeft()I

    move-result v3

    iput v3, v1, Landroid/graphics/Rect;->right:I

    invoke-direct {p0, p1, v1}, Lnet/gogame/gopay/sdk/support/c;->a(Landroid/graphics/Canvas;Landroid/graphics/Rect;)V

    :cond_3
    add-int/lit8 v2, v2, 0x1

    goto :goto_0

    :cond_4
    return-void
.end method

.method protected final onLayout(ZIIII)V
    .locals 10
    .annotation build Landroid/annotation/SuppressLint;
        value = {
            "WrongCall"
        }
    .end annotation

    :goto_0
    invoke-super/range {p0 .. p5}, Landroid/widget/AdapterView;->onLayout(ZIIII)V

    iget-object v0, p0, Lnet/gogame/gopay/sdk/support/c;->b:Landroid/widget/ListAdapter;

    if-nez v0, :cond_0

    return-void

    :cond_0
    invoke-virtual {p0}, Lnet/gogame/gopay/sdk/support/c;->invalidate()V

    iget-boolean v0, p0, Lnet/gogame/gopay/sdk/support/c;->i:Z

    const/4 v1, 0x0

    if-eqz v0, :cond_1

    iget v0, p0, Lnet/gogame/gopay/sdk/support/c;->c:I

    invoke-direct {p0}, Lnet/gogame/gopay/sdk/support/c;->a()V

    invoke-virtual {p0}, Lnet/gogame/gopay/sdk/support/c;->removeAllViewsInLayout()V

    iput v0, p0, Lnet/gogame/gopay/sdk/support/c;->d:I

    iput-boolean v1, p0, Lnet/gogame/gopay/sdk/support/c;->i:Z

    :cond_1
    iget-object v0, p0, Lnet/gogame/gopay/sdk/support/c;->n:Ljava/lang/Integer;

    if-eqz v0, :cond_2

    iget-object v0, p0, Lnet/gogame/gopay/sdk/support/c;->n:Ljava/lang/Integer;

    invoke-virtual {v0}, Ljava/lang/Integer;->intValue()I

    move-result v0

    iput v0, p0, Lnet/gogame/gopay/sdk/support/c;->d:I

    const/4 v0, 0x0

    iput-object v0, p0, Lnet/gogame/gopay/sdk/support/c;->n:Ljava/lang/Integer;

    :cond_2
    iget-object v0, p0, Lnet/gogame/gopay/sdk/support/c;->a:Landroid/widget/Scroller;

    invoke-virtual {v0}, Landroid/widget/Scroller;->computeScrollOffset()Z

    move-result v0

    if-eqz v0, :cond_3

    iget-object v0, p0, Lnet/gogame/gopay/sdk/support/c;->a:Landroid/widget/Scroller;

    invoke-virtual {v0}, Landroid/widget/Scroller;->getCurrX()I

    move-result v0

    iput v0, p0, Lnet/gogame/gopay/sdk/support/c;->d:I

    :cond_3
    iget v0, p0, Lnet/gogame/gopay/sdk/support/c;->d:I

    const/4 v2, 0x1

    if-gez v0, :cond_5

    iput v1, p0, Lnet/gogame/gopay/sdk/support/c;->d:I

    iget-boolean v0, p0, Lnet/gogame/gopay/sdk/support/c;->D:Z

    if-eqz v0, :cond_4

    iget-object v0, p0, Lnet/gogame/gopay/sdk/support/c;->x:Landroidx/core/widget/EdgeEffectCompat;

    invoke-virtual {v0}, Landroidx/core/widget/EdgeEffectCompat;->isFinished()Z

    move-result v0

    if-eqz v0, :cond_4

    iget-object v0, p0, Lnet/gogame/gopay/sdk/support/c;->x:Landroidx/core/widget/EdgeEffectCompat;

    :goto_1
    invoke-direct {p0}, Lnet/gogame/gopay/sdk/support/c;->c()F

    move-result v3

    float-to-int v3, v3

    invoke-virtual {v0, v3}, Landroidx/core/widget/EdgeEffectCompat;->onAbsorb(I)Z

    :cond_4
    iget-object v0, p0, Lnet/gogame/gopay/sdk/support/c;->a:Landroid/widget/Scroller;

    invoke-virtual {v0, v2}, Landroid/widget/Scroller;->forceFinished(Z)V

    sget v0, Lnet/gogame/gopay/sdk/support/k;->a:I

    invoke-direct {p0, v0}, Lnet/gogame/gopay/sdk/support/c;->setCurrentScrollState$6c40596b(I)V

    goto :goto_2

    :cond_5
    iget v0, p0, Lnet/gogame/gopay/sdk/support/c;->d:I

    iget v3, p0, Lnet/gogame/gopay/sdk/support/c;->o:I

    if-le v0, v3, :cond_6

    iget v0, p0, Lnet/gogame/gopay/sdk/support/c;->o:I

    iput v0, p0, Lnet/gogame/gopay/sdk/support/c;->d:I

    iget-boolean v0, p0, Lnet/gogame/gopay/sdk/support/c;->D:Z

    if-eqz v0, :cond_4

    iget-object v0, p0, Lnet/gogame/gopay/sdk/support/c;->y:Landroidx/core/widget/EdgeEffectCompat;

    invoke-virtual {v0}, Landroidx/core/widget/EdgeEffectCompat;->isFinished()Z

    move-result v0

    if-eqz v0, :cond_4

    iget-object v0, p0, Lnet/gogame/gopay/sdk/support/c;->y:Landroidx/core/widget/EdgeEffectCompat;

    goto :goto_1

    :cond_6
    :goto_2
    iget v0, p0, Lnet/gogame/gopay/sdk/support/c;->c:I

    iget v3, p0, Lnet/gogame/gopay/sdk/support/c;->d:I

    sub-int/2addr v0, v3

    :goto_3
    invoke-direct {p0}, Lnet/gogame/gopay/sdk/support/c;->getLeftmostChild()Landroid/view/View;

    move-result-object v3

    if-eqz v3, :cond_8

    invoke-virtual {v3}, Landroid/view/View;->getRight()I

    move-result v4

    add-int/2addr v4, v0

    if-gtz v4, :cond_8

    iget v4, p0, Lnet/gogame/gopay/sdk/support/c;->g:I

    iget v5, p0, Lnet/gogame/gopay/sdk/support/c;->p:I

    invoke-direct {p0, v5}, Lnet/gogame/gopay/sdk/support/c;->c(I)Z

    move-result v5

    if-eqz v5, :cond_7

    invoke-virtual {v3}, Landroid/view/View;->getMeasuredWidth()I

    move-result v5

    goto :goto_4

    :cond_7
    iget v5, p0, Lnet/gogame/gopay/sdk/support/c;->l:I

    invoke-virtual {v3}, Landroid/view/View;->getMeasuredWidth()I

    move-result v6

    add-int/2addr v5, v6

    :goto_4
    add-int/2addr v4, v5

    iput v4, p0, Lnet/gogame/gopay/sdk/support/c;->g:I

    iget v4, p0, Lnet/gogame/gopay/sdk/support/c;->p:I

    invoke-direct {p0, v4, v3}, Lnet/gogame/gopay/sdk/support/c;->a(ILandroid/view/View;)V

    invoke-virtual {p0, v3}, Lnet/gogame/gopay/sdk/support/c;->removeViewInLayout(Landroid/view/View;)V

    iget v3, p0, Lnet/gogame/gopay/sdk/support/c;->p:I

    add-int/2addr v3, v2

    iput v3, p0, Lnet/gogame/gopay/sdk/support/c;->p:I

    goto :goto_3

    :cond_8
    :goto_5
    invoke-direct {p0}, Lnet/gogame/gopay/sdk/support/c;->getRightmostChild()Landroid/view/View;

    move-result-object v3

    if-eqz v3, :cond_9

    invoke-virtual {v3}, Landroid/view/View;->getLeft()I

    move-result v4

    add-int/2addr v4, v0

    invoke-virtual {p0}, Lnet/gogame/gopay/sdk/support/c;->getWidth()I

    move-result v5

    if-lt v4, v5, :cond_9

    iget v4, p0, Lnet/gogame/gopay/sdk/support/c;->q:I

    invoke-direct {p0, v4, v3}, Lnet/gogame/gopay/sdk/support/c;->a(ILandroid/view/View;)V

    invoke-virtual {p0, v3}, Lnet/gogame/gopay/sdk/support/c;->removeViewInLayout(Landroid/view/View;)V

    iget v3, p0, Lnet/gogame/gopay/sdk/support/c;->q:I

    sub-int/2addr v3, v2

    iput v3, p0, Lnet/gogame/gopay/sdk/support/c;->q:I

    goto :goto_5

    :cond_9
    invoke-direct {p0}, Lnet/gogame/gopay/sdk/support/c;->getRightmostChild()Landroid/view/View;

    move-result-object v3

    if-eqz v3, :cond_a

    invoke-virtual {v3}, Landroid/view/View;->getRight()I

    move-result v3

    goto :goto_6

    :cond_a
    const/4 v3, 0x0

    :cond_b
    :goto_6
    add-int v4, v3, v0

    iget v5, p0, Lnet/gogame/gopay/sdk/support/c;->l:I

    add-int/2addr v4, v5

    invoke-virtual {p0}, Lnet/gogame/gopay/sdk/support/c;->getWidth()I

    move-result v5

    if-ge v4, v5, :cond_e

    iget v4, p0, Lnet/gogame/gopay/sdk/support/c;->q:I

    add-int/2addr v4, v2

    iget-object v5, p0, Lnet/gogame/gopay/sdk/support/c;->b:Landroid/widget/ListAdapter;

    invoke-interface {v5}, Landroid/widget/ListAdapter;->getCount()I

    move-result v5

    if-ge v4, v5, :cond_e

    iget v4, p0, Lnet/gogame/gopay/sdk/support/c;->q:I

    add-int/2addr v4, v2

    iput v4, p0, Lnet/gogame/gopay/sdk/support/c;->q:I

    iget v4, p0, Lnet/gogame/gopay/sdk/support/c;->p:I

    if-gez v4, :cond_c

    iget v4, p0, Lnet/gogame/gopay/sdk/support/c;->q:I

    iput v4, p0, Lnet/gogame/gopay/sdk/support/c;->p:I

    :cond_c
    iget-object v4, p0, Lnet/gogame/gopay/sdk/support/c;->b:Landroid/widget/ListAdapter;

    iget v5, p0, Lnet/gogame/gopay/sdk/support/c;->q:I

    iget v6, p0, Lnet/gogame/gopay/sdk/support/c;->q:I

    invoke-direct {p0, v6}, Lnet/gogame/gopay/sdk/support/c;->a(I)Landroid/view/View;

    move-result-object v6

    invoke-interface {v4, v5, v6, p0}, Landroid/widget/ListAdapter;->getView(ILandroid/view/View;Landroid/view/ViewGroup;)Landroid/view/View;

    move-result-object v4

    const/4 v5, -0x1

    invoke-direct {p0, v4, v5}, Lnet/gogame/gopay/sdk/support/c;->a(Landroid/view/View;I)V

    iget v5, p0, Lnet/gogame/gopay/sdk/support/c;->q:I

    if-nez v5, :cond_d

    const/4 v5, 0x0

    goto :goto_7

    :cond_d
    iget v5, p0, Lnet/gogame/gopay/sdk/support/c;->l:I

    :goto_7
    invoke-virtual {v4}, Landroid/view/View;->getMeasuredWidth()I

    move-result v4

    add-int/2addr v5, v4

    add-int/2addr v3, v5

    iget-object v4, p0, Lnet/gogame/gopay/sdk/support/c;->s:Lnet/gogame/gopay/sdk/support/l;

    if-eqz v4, :cond_b

    iget-object v4, p0, Lnet/gogame/gopay/sdk/support/c;->b:Landroid/widget/ListAdapter;

    if-eqz v4, :cond_b

    iget-object v4, p0, Lnet/gogame/gopay/sdk/support/c;->b:Landroid/widget/ListAdapter;

    invoke-interface {v4}, Landroid/widget/ListAdapter;->getCount()I

    move-result v4

    iget v5, p0, Lnet/gogame/gopay/sdk/support/c;->q:I

    add-int/2addr v5, v2

    sub-int/2addr v4, v5

    iget v5, p0, Lnet/gogame/gopay/sdk/support/c;->t:I

    if-ge v4, v5, :cond_b

    iget-boolean v4, p0, Lnet/gogame/gopay/sdk/support/c;->u:Z

    if-nez v4, :cond_b

    iput-boolean v2, p0, Lnet/gogame/gopay/sdk/support/c;->u:Z

    goto :goto_6

    :cond_e
    invoke-direct {p0}, Lnet/gogame/gopay/sdk/support/c;->getLeftmostChild()Landroid/view/View;

    move-result-object v3

    if-eqz v3, :cond_f

    invoke-virtual {v3}, Landroid/view/View;->getLeft()I

    move-result v3

    goto :goto_8

    :cond_f
    const/4 v3, 0x0

    :goto_8
    add-int v4, v3, v0

    iget v5, p0, Lnet/gogame/gopay/sdk/support/c;->l:I

    sub-int/2addr v4, v5

    if-lez v4, :cond_12

    iget v4, p0, Lnet/gogame/gopay/sdk/support/c;->p:I

    if-lez v4, :cond_12

    iget v4, p0, Lnet/gogame/gopay/sdk/support/c;->p:I

    sub-int/2addr v4, v2

    iput v4, p0, Lnet/gogame/gopay/sdk/support/c;->p:I

    iget-object v4, p0, Lnet/gogame/gopay/sdk/support/c;->b:Landroid/widget/ListAdapter;

    iget v5, p0, Lnet/gogame/gopay/sdk/support/c;->p:I

    iget v6, p0, Lnet/gogame/gopay/sdk/support/c;->p:I

    invoke-direct {p0, v6}, Lnet/gogame/gopay/sdk/support/c;->a(I)Landroid/view/View;

    move-result-object v6

    invoke-interface {v4, v5, v6, p0}, Landroid/widget/ListAdapter;->getView(ILandroid/view/View;Landroid/view/ViewGroup;)Landroid/view/View;

    move-result-object v4

    invoke-direct {p0, v4, v1}, Lnet/gogame/gopay/sdk/support/c;->a(Landroid/view/View;I)V

    iget v5, p0, Lnet/gogame/gopay/sdk/support/c;->p:I

    if-nez v5, :cond_10

    invoke-virtual {v4}, Landroid/view/View;->getMeasuredWidth()I

    move-result v5

    goto :goto_9

    :cond_10
    iget v5, p0, Lnet/gogame/gopay/sdk/support/c;->l:I

    invoke-virtual {v4}, Landroid/view/View;->getMeasuredWidth()I

    move-result v6

    add-int/2addr v5, v6

    :goto_9
    sub-int/2addr v3, v5

    iget v5, p0, Lnet/gogame/gopay/sdk/support/c;->g:I

    add-int v6, v3, v0

    if-nez v6, :cond_11

    invoke-virtual {v4}, Landroid/view/View;->getMeasuredWidth()I

    move-result v4

    goto :goto_a

    :cond_11
    iget v6, p0, Lnet/gogame/gopay/sdk/support/c;->l:I

    invoke-virtual {v4}, Landroid/view/View;->getMeasuredWidth()I

    move-result v4

    add-int/2addr v4, v6

    :goto_a
    sub-int/2addr v5, v4

    iput v5, p0, Lnet/gogame/gopay/sdk/support/c;->g:I

    goto :goto_8

    :cond_12
    invoke-virtual {p0}, Lnet/gogame/gopay/sdk/support/c;->getChildCount()I

    move-result v3

    iget-boolean v4, p0, Lnet/gogame/gopay/sdk/support/c;->D:Z

    if-nez v4, :cond_13

    iput v1, p0, Lnet/gogame/gopay/sdk/support/c;->g:I

    const/4 v0, 0x0

    :cond_13
    if-lez v3, :cond_14

    iget v4, p0, Lnet/gogame/gopay/sdk/support/c;->g:I

    add-int/2addr v4, v0

    iput v4, p0, Lnet/gogame/gopay/sdk/support/c;->g:I

    iget v0, p0, Lnet/gogame/gopay/sdk/support/c;->g:I

    move v4, v0

    const/4 v0, 0x0

    :goto_b
    if-ge v0, v3, :cond_14

    invoke-virtual {p0, v0}, Lnet/gogame/gopay/sdk/support/c;->getChildAt(I)Landroid/view/View;

    move-result-object v5

    invoke-virtual {p0}, Lnet/gogame/gopay/sdk/support/c;->getPaddingLeft()I

    move-result v6

    add-int/2addr v6, v4

    invoke-virtual {p0}, Lnet/gogame/gopay/sdk/support/c;->getPaddingTop()I

    move-result v7

    invoke-virtual {v5}, Landroid/view/View;->getMeasuredWidth()I

    move-result v8

    add-int/2addr v8, v6

    invoke-virtual {v5}, Landroid/view/View;->getMeasuredHeight()I

    move-result v9

    add-int/2addr v9, v7

    invoke-virtual {v5, v6, v7, v8, v9}, Landroid/view/View;->layout(IIII)V

    invoke-virtual {v5}, Landroid/view/View;->getMeasuredWidth()I

    move-result v5

    iget v6, p0, Lnet/gogame/gopay/sdk/support/c;->l:I

    add-int/2addr v5, v6

    add-int/2addr v4, v5

    add-int/lit8 v0, v0, 0x1

    goto :goto_b

    :cond_14
    iget v0, p0, Lnet/gogame/gopay/sdk/support/c;->d:I

    iput v0, p0, Lnet/gogame/gopay/sdk/support/c;->c:I

    iget v0, p0, Lnet/gogame/gopay/sdk/support/c;->q:I

    invoke-direct {p0, v0}, Lnet/gogame/gopay/sdk/support/c;->c(I)Z

    move-result v0

    if-eqz v0, :cond_16

    invoke-direct {p0}, Lnet/gogame/gopay/sdk/support/c;->getRightmostChild()Landroid/view/View;

    move-result-object v0

    if-eqz v0, :cond_16

    iget v3, p0, Lnet/gogame/gopay/sdk/support/c;->o:I

    iget v4, p0, Lnet/gogame/gopay/sdk/support/c;->c:I

    invoke-virtual {v0}, Landroid/view/View;->getRight()I

    move-result v0

    invoke-virtual {p0}, Lnet/gogame/gopay/sdk/support/c;->getPaddingLeft()I

    move-result v5

    sub-int/2addr v0, v5

    add-int/2addr v4, v0

    invoke-direct {p0}, Lnet/gogame/gopay/sdk/support/c;->getRenderWidth()I

    move-result v0

    sub-int/2addr v4, v0

    iput v4, p0, Lnet/gogame/gopay/sdk/support/c;->o:I

    iget v0, p0, Lnet/gogame/gopay/sdk/support/c;->o:I

    if-gez v0, :cond_15

    iput v1, p0, Lnet/gogame/gopay/sdk/support/c;->o:I

    :cond_15
    iget v0, p0, Lnet/gogame/gopay/sdk/support/c;->o:I

    if-eq v0, v3, :cond_16

    const/4 v1, 0x1

    :cond_16
    if-eqz v1, :cond_17

    goto/16 :goto_0

    :cond_17
    iget-object p1, p0, Lnet/gogame/gopay/sdk/support/c;->a:Landroid/widget/Scroller;

    invoke-virtual {p1}, Landroid/widget/Scroller;->isFinished()Z

    move-result p1

    if-eqz p1, :cond_18

    iget p1, p0, Lnet/gogame/gopay/sdk/support/c;->w:I

    sget p2, Lnet/gogame/gopay/sdk/support/k;->c:I

    if-ne p1, p2, :cond_19

    sget p1, Lnet/gogame/gopay/sdk/support/k;->a:I

    invoke-direct {p0, p1}, Lnet/gogame/gopay/sdk/support/c;->setCurrentScrollState$6c40596b(I)V

    return-void

    :cond_18
    iget-object p1, p0, Lnet/gogame/gopay/sdk/support/c;->F:Ljava/lang/Runnable;

    invoke-static {p0, p1}, Landroidx/core/view/ViewCompat;->postOnAnimation(Landroid/view/View;Ljava/lang/Runnable;)V

    :cond_19
    return-void
.end method

.method protected final onMeasure(II)V
    .locals 0

    invoke-super {p0, p1, p2}, Landroid/widget/AdapterView;->onMeasure(II)V

    iput p2, p0, Lnet/gogame/gopay/sdk/support/c;->z:I

    return-void
.end method

.method public final onRestoreInstanceState(Landroid/os/Parcelable;)V
    .locals 1

    instance-of v0, p1, Landroid/os/Bundle;

    if-eqz v0, :cond_0

    check-cast p1, Landroid/os/Bundle;

    const-string v0, "BUNDLE_ID_CURRENT_X"

    invoke-virtual {p1, v0}, Landroid/os/Bundle;->getInt(Ljava/lang/String;)I

    move-result v0

    invoke-static {v0}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v0

    iput-object v0, p0, Lnet/gogame/gopay/sdk/support/c;->n:Ljava/lang/Integer;

    const-string v0, "BUNDLE_ID_PARENT_STATE"

    invoke-virtual {p1, v0}, Landroid/os/Bundle;->getParcelable(Ljava/lang/String;)Landroid/os/Parcelable;

    move-result-object p1

    invoke-super {p0, p1}, Landroid/widget/AdapterView;->onRestoreInstanceState(Landroid/os/Parcelable;)V

    :cond_0
    return-void
.end method

.method public final onSaveInstanceState()Landroid/os/Parcelable;
    .locals 3

    new-instance v0, Landroid/os/Bundle;

    invoke-direct {v0}, Landroid/os/Bundle;-><init>()V

    const-string v1, "BUNDLE_ID_PARENT_STATE"

    invoke-super {p0}, Landroid/widget/AdapterView;->onSaveInstanceState()Landroid/os/Parcelable;

    move-result-object v2

    invoke-virtual {v0, v1, v2}, Landroid/os/Bundle;->putParcelable(Ljava/lang/String;Landroid/os/Parcelable;)V

    const-string v1, "BUNDLE_ID_CURRENT_X"

    iget v2, p0, Lnet/gogame/gopay/sdk/support/c;->c:I

    invoke-virtual {v0, v1, v2}, Landroid/os/Bundle;->putInt(Ljava/lang/String;I)V

    return-object v0
.end method

.method public final onTouchEvent(Landroid/view/MotionEvent;)Z
    .locals 3

    invoke-virtual {p1}, Landroid/view/MotionEvent;->getAction()I

    move-result v0

    const/4 v1, 0x0

    const/4 v2, 0x1

    if-ne v0, v2, :cond_2

    iget-object v0, p0, Lnet/gogame/gopay/sdk/support/c;->a:Landroid/widget/Scroller;

    if-eqz v0, :cond_0

    iget-object v0, p0, Lnet/gogame/gopay/sdk/support/c;->a:Landroid/widget/Scroller;

    invoke-virtual {v0}, Landroid/widget/Scroller;->isFinished()Z

    move-result v0

    if-eqz v0, :cond_1

    :cond_0
    sget v0, Lnet/gogame/gopay/sdk/support/k;->a:I

    invoke-direct {p0, v0}, Lnet/gogame/gopay/sdk/support/c;->setCurrentScrollState$6c40596b(I)V

    :cond_1
    invoke-static {v1}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v0

    invoke-direct {p0, v0}, Lnet/gogame/gopay/sdk/support/c;->a(Ljava/lang/Boolean;)V

    invoke-direct {p0}, Lnet/gogame/gopay/sdk/support/c;->e()V

    goto :goto_0

    :cond_2
    invoke-virtual {p1}, Landroid/view/MotionEvent;->getAction()I

    move-result v0

    const/4 v2, 0x3

    if-ne v0, v2, :cond_3

    invoke-direct {p0}, Lnet/gogame/gopay/sdk/support/c;->d()V

    invoke-direct {p0}, Lnet/gogame/gopay/sdk/support/c;->e()V

    invoke-static {v1}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v0

    invoke-direct {p0, v0}, Lnet/gogame/gopay/sdk/support/c;->a(Ljava/lang/Boolean;)V

    :cond_3
    :goto_0
    invoke-super {p0, p1}, Landroid/widget/AdapterView;->onTouchEvent(Landroid/view/MotionEvent;)Z

    move-result p1

    return p1
.end method

.method public final bridge synthetic setAdapter(Landroid/widget/Adapter;)V
    .locals 0

    check-cast p1, Landroid/widget/ListAdapter;

    invoke-virtual {p0, p1}, Lnet/gogame/gopay/sdk/support/c;->setAdapter(Landroid/widget/ListAdapter;)V

    return-void
.end method

.method public final setAdapter(Landroid/widget/ListAdapter;)V
    .locals 3

    iget-object v0, p0, Lnet/gogame/gopay/sdk/support/c;->b:Landroid/widget/ListAdapter;

    if-eqz v0, :cond_0

    iget-object v0, p0, Lnet/gogame/gopay/sdk/support/c;->b:Landroid/widget/ListAdapter;

    iget-object v1, p0, Lnet/gogame/gopay/sdk/support/c;->E:Landroid/database/DataSetObserver;

    invoke-interface {v0, v1}, Landroid/widget/ListAdapter;->unregisterDataSetObserver(Landroid/database/DataSetObserver;)V

    :cond_0
    const/4 v0, 0x0

    if-eqz p1, :cond_1

    iput-boolean v0, p0, Lnet/gogame/gopay/sdk/support/c;->u:Z

    iput-object p1, p0, Lnet/gogame/gopay/sdk/support/c;->b:Landroid/widget/ListAdapter;

    iget-object p1, p0, Lnet/gogame/gopay/sdk/support/c;->b:Landroid/widget/ListAdapter;

    iget-object v1, p0, Lnet/gogame/gopay/sdk/support/c;->E:Landroid/database/DataSetObserver;

    invoke-interface {p1, v1}, Landroid/widget/ListAdapter;->registerDataSetObserver(Landroid/database/DataSetObserver;)V

    :cond_1
    iget-object p1, p0, Lnet/gogame/gopay/sdk/support/c;->b:Landroid/widget/ListAdapter;

    invoke-interface {p1}, Landroid/widget/ListAdapter;->getViewTypeCount()I

    move-result p1

    iget-object v1, p0, Lnet/gogame/gopay/sdk/support/c;->h:Ljava/util/List;

    invoke-interface {v1}, Ljava/util/List;->clear()V

    :goto_0
    if-ge v0, p1, :cond_2

    iget-object v1, p0, Lnet/gogame/gopay/sdk/support/c;->h:Ljava/util/List;

    new-instance v2, Ljava/util/LinkedList;

    invoke-direct {v2}, Ljava/util/LinkedList;-><init>()V

    invoke-interface {v1, v2}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    add-int/lit8 v0, v0, 0x1

    goto :goto_0

    :cond_2
    invoke-direct {p0}, Lnet/gogame/gopay/sdk/support/c;->b()V

    return-void
.end method

.method public final setDivider(Landroid/graphics/drawable/Drawable;)V
    .locals 0

    iput-object p1, p0, Lnet/gogame/gopay/sdk/support/c;->m:Landroid/graphics/drawable/Drawable;

    if-eqz p1, :cond_0

    invoke-virtual {p1}, Landroid/graphics/drawable/Drawable;->getIntrinsicWidth()I

    move-result p1

    :goto_0
    invoke-virtual {p0, p1}, Lnet/gogame/gopay/sdk/support/c;->setDividerWidth(I)V

    return-void

    :cond_0
    const/4 p1, 0x0

    goto :goto_0
.end method

.method public final setDividerWidth(I)V
    .locals 0

    iput p1, p0, Lnet/gogame/gopay/sdk/support/c;->l:I

    invoke-virtual {p0}, Lnet/gogame/gopay/sdk/support/c;->requestLayout()V

    invoke-virtual {p0}, Lnet/gogame/gopay/sdk/support/c;->invalidate()V

    return-void
.end method

.method public final setOnClickListener(Landroid/view/View$OnClickListener;)V
    .locals 0

    iput-object p1, p0, Lnet/gogame/gopay/sdk/support/c;->C:Landroid/view/View$OnClickListener;

    return-void
.end method

.method public final setOnScrollStateChangedListener(Lnet/gogame/gopay/sdk/support/j;)V
    .locals 0

    iput-object p1, p0, Lnet/gogame/gopay/sdk/support/c;->v:Lnet/gogame/gopay/sdk/support/j;

    return-void
.end method

.method public final setScrollingEnabled(Z)V
    .locals 0

    iput-boolean p1, p0, Lnet/gogame/gopay/sdk/support/c;->D:Z

    return-void
.end method

.method public final setSelection(I)V
    .locals 0

    iput p1, p0, Lnet/gogame/gopay/sdk/support/c;->r:I

    return-void
.end method
