.class public abstract Lnet/gogame/gowrap/ui/fab/AbstractFab;
.super Ljava/lang/Object;
.source "AbstractFab.java"

# interfaces
.implements Lnet/gogame/gowrap/ui/fab/Fab;


# instance fields
.field private clickListener:Lnet/gogame/gowrap/ui/fab/Fab$ClickListener;


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 13
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method protected createImageView(Landroid/content/Context;I)Landroid/view/View;
    .locals 5

    .line 23
    new-instance v0, Landroid/widget/RelativeLayout;

    invoke-direct {v0, p1}, Landroid/widget/RelativeLayout;-><init>(Landroid/content/Context;)V

    const/high16 v1, 0x41000000    # 8.0f

    .line 24
    invoke-static {p1, v1}, Lnet/gogame/gowrap/ui/utils/DisplayUtils;->pxFromDp(Landroid/content/Context;F)I

    move-result v1

    const/4 v2, 0x0

    invoke-virtual {v0, v2, v1, v2, v2}, Landroid/widget/RelativeLayout;->setPadding(IIII)V

    .line 25
    invoke-virtual {v0, v2}, Landroid/widget/RelativeLayout;->setClipToPadding(Z)V

    .line 26
    new-instance v1, Landroid/widget/RelativeLayout$LayoutParams;

    const/high16 v3, 0x43480000    # 200.0f

    .line 28
    invoke-static {p1, v3}, Lnet/gogame/gowrap/ui/utils/DisplayUtils;->pxFromDp(Landroid/content/Context;F)I

    move-result v3

    const/4 v4, -0x2

    invoke-direct {v1, v4, v3}, Landroid/widget/RelativeLayout$LayoutParams;-><init>(II)V

    .line 29
    invoke-virtual {v0, v1}, Landroid/widget/RelativeLayout;->setLayoutParams(Landroid/view/ViewGroup$LayoutParams;)V

    .line 32
    new-instance v1, Landroid/widget/ImageView;

    invoke-direct {v1, p1}, Landroid/widget/ImageView;-><init>(Landroid/content/Context;)V

    .line 33
    invoke-virtual {p1}, Landroid/content/Context;->getResources()Landroid/content/res/Resources;

    move-result-object v3

    invoke-virtual {v3, p2}, Landroid/content/res/Resources;->getDrawable(I)Landroid/graphics/drawable/Drawable;

    move-result-object p2

    .line 34
    invoke-virtual {v1, p2}, Landroid/widget/ImageView;->setImageDrawable(Landroid/graphics/drawable/Drawable;)V

    .line 35
    invoke-virtual {v0, v1}, Landroid/widget/RelativeLayout;->addView(Landroid/view/View;)V

    .line 38
    new-instance p2, Landroid/widget/ImageView;

    invoke-direct {p2, p1}, Landroid/widget/ImageView;-><init>(Landroid/content/Context;)V

    .line 39
    sget v1, Lnet/gogame/gowrap/R$drawable;->net_gogame_gowrap_server_down_fab_icon:I

    invoke-virtual {p2, v1}, Landroid/widget/ImageView;->setImageResource(I)V

    .line 40
    new-instance v1, Landroid/widget/RelativeLayout$LayoutParams;

    invoke-direct {v1, v4, v4}, Landroid/widget/RelativeLayout$LayoutParams;-><init>(II)V

    const/high16 v3, 0x41a00000    # 20.0f

    .line 43
    invoke-static {p1, v3}, Lnet/gogame/gowrap/ui/utils/DisplayUtils;->pxFromDp(Landroid/content/Context;F)I

    move-result v3

    const/high16 v4, 0x40a00000    # 5.0f

    invoke-static {p1, v4}, Lnet/gogame/gowrap/ui/utils/DisplayUtils;->pxFromDp(Landroid/content/Context;F)I

    move-result p1

    invoke-virtual {v1, v3, p1, v2, v2}, Landroid/widget/RelativeLayout$LayoutParams;->setMargins(IIII)V

    .line 44
    invoke-virtual {p2, v1}, Landroid/widget/ImageView;->setLayoutParams(Landroid/view/ViewGroup$LayoutParams;)V

    const/16 p1, 0x8

    .line 45
    invoke-virtual {p2, p1}, Landroid/widget/ImageView;->setVisibility(I)V

    .line 46
    invoke-virtual {v0, p2}, Landroid/widget/RelativeLayout;->addView(Landroid/view/View;)V

    return-object v0
.end method

.method protected fireClickListener(Landroid/view/MotionEvent;)V
    .locals 1

    .line 17
    iget-object v0, p0, Lnet/gogame/gowrap/ui/fab/AbstractFab;->clickListener:Lnet/gogame/gowrap/ui/fab/Fab$ClickListener;

    if-eqz v0, :cond_0

    .line 18
    iget-object v0, p0, Lnet/gogame/gowrap/ui/fab/AbstractFab;->clickListener:Lnet/gogame/gowrap/ui/fab/Fab$ClickListener;

    invoke-interface {v0, p0, p1}, Lnet/gogame/gowrap/ui/fab/Fab$ClickListener;->onClick(Lnet/gogame/gowrap/ui/fab/Fab;Landroid/view/MotionEvent;)V

    :cond_0
    return-void
.end method

.method public setClickListener(Lnet/gogame/gowrap/ui/fab/Fab$ClickListener;)V
    .locals 0

    .line 52
    iput-object p1, p0, Lnet/gogame/gowrap/ui/fab/AbstractFab;->clickListener:Lnet/gogame/gowrap/ui/fab/Fab$ClickListener;

    return-void
.end method
