.class public final Lnet/gogame/gowrap/ui/utils/UIUtils;
.super Ljava/lang/Object;
.source "UIUtils.java"


# direct methods
.method private constructor <init>()V
    .locals 0

    .line 12
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method static synthetic access$000(Landroid/widget/EditText;IILjava/lang/CharSequence;)V
    .locals 0

    .line 9
    invoke-static {p0, p1, p2, p3}, Lnet/gogame/gowrap/ui/utils/UIUtils;->setupRightDrawable(Landroid/widget/EditText;IILjava/lang/CharSequence;)V

    return-void
.end method

.method public static setupRightDrawable(Landroid/content/Context;Landroid/widget/EditText;I)V
    .locals 2

    .line 17
    invoke-virtual {p0}, Landroid/content/Context;->getResources()Landroid/content/res/Resources;

    move-result-object p0

    invoke-virtual {p0, p2}, Landroid/content/res/Resources;->obtainTypedArray(I)Landroid/content/res/TypedArray;

    move-result-object p0

    const/4 p2, 0x0

    .line 19
    invoke-virtual {p0, p2, p2}, Landroid/content/res/TypedArray;->getResourceId(II)I

    move-result v0

    const/4 v1, 0x1

    .line 20
    invoke-virtual {p0, v1, p2}, Landroid/content/res/TypedArray;->getResourceId(II)I

    move-result p2

    .line 21
    invoke-virtual {p0}, Landroid/content/res/TypedArray;->recycle()V

    .line 24
    invoke-virtual {p1}, Landroid/widget/EditText;->getText()Landroid/text/Editable;

    move-result-object p0

    .line 23
    invoke-static {p1, v0, p2, p0}, Lnet/gogame/gowrap/ui/utils/UIUtils;->setupRightDrawable(Landroid/widget/EditText;IILjava/lang/CharSequence;)V

    .line 25
    new-instance p0, Lnet/gogame/gowrap/ui/utils/UIUtils$1;

    invoke-direct {p0, p1, v0, p2}, Lnet/gogame/gowrap/ui/utils/UIUtils$1;-><init>(Landroid/widget/EditText;II)V

    invoke-virtual {p1, p0}, Landroid/widget/EditText;->addTextChangedListener(Landroid/text/TextWatcher;)V

    return-void
.end method

.method private static setupRightDrawable(Landroid/widget/EditText;IILjava/lang/CharSequence;)V
    .locals 1

    const/4 v0, 0x0

    if-eqz p3, :cond_0

    .line 47
    invoke-interface {p3}, Ljava/lang/CharSequence;->length()I

    move-result p3

    if-lez p3, :cond_0

    .line 48
    invoke-virtual {p0, v0, v0, p2, v0}, Landroid/widget/EditText;->setCompoundDrawablesWithIntrinsicBounds(IIII)V

    goto :goto_0

    .line 51
    :cond_0
    invoke-virtual {p0, v0, v0, p1, v0}, Landroid/widget/EditText;->setCompoundDrawablesWithIntrinsicBounds(IIII)V

    :goto_0
    return-void
.end method
