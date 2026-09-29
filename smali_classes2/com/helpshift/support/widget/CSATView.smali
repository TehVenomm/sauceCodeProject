.class public Lcom/helpshift/support/widget/CSATView;
.super Landroid/widget/RelativeLayout;
.source "CSATView.java"

# interfaces
.implements Landroid/widget/RatingBar$OnRatingBarChangeListener;


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/helpshift/support/widget/CSATView$CSATListener;
    }
.end annotation


# instance fields
.field private csatDialog:Lcom/helpshift/support/widget/CSATDialog;

.field private csatListener:Lcom/helpshift/support/widget/CSATView$CSATListener;

.field private ratingBar:Landroid/widget/RatingBar;


# direct methods
.method public constructor <init>(Landroid/content/Context;)V
    .locals 1

    .line 19
    invoke-direct {p0, p1}, Landroid/widget/RelativeLayout;-><init>(Landroid/content/Context;)V

    const/4 v0, 0x0

    .line 16
    iput-object v0, p0, Lcom/helpshift/support/widget/CSATView;->csatListener:Lcom/helpshift/support/widget/CSATView$CSATListener;

    .line 20
    invoke-direct {p0, p1}, Lcom/helpshift/support/widget/CSATView;->initView(Landroid/content/Context;)V

    return-void
.end method

.method public constructor <init>(Landroid/content/Context;Landroid/util/AttributeSet;)V
    .locals 0

    .line 24
    invoke-direct {p0, p1, p2}, Landroid/widget/RelativeLayout;-><init>(Landroid/content/Context;Landroid/util/AttributeSet;)V

    const/4 p2, 0x0

    .line 16
    iput-object p2, p0, Lcom/helpshift/support/widget/CSATView;->csatListener:Lcom/helpshift/support/widget/CSATView$CSATListener;

    .line 25
    invoke-direct {p0, p1}, Lcom/helpshift/support/widget/CSATView;->initView(Landroid/content/Context;)V

    return-void
.end method

.method public constructor <init>(Landroid/content/Context;Landroid/util/AttributeSet;I)V
    .locals 0

    .line 29
    invoke-direct {p0, p1, p2, p3}, Landroid/widget/RelativeLayout;-><init>(Landroid/content/Context;Landroid/util/AttributeSet;I)V

    const/4 p2, 0x0

    .line 16
    iput-object p2, p0, Lcom/helpshift/support/widget/CSATView;->csatListener:Lcom/helpshift/support/widget/CSATView$CSATListener;

    .line 30
    invoke-direct {p0, p1}, Lcom/helpshift/support/widget/CSATView;->initView(Landroid/content/Context;)V

    return-void
.end method

.method private initView(Landroid/content/Context;)V
    .locals 1

    .line 34
    sget v0, Lcom/helpshift/R$layout;->hs__csat_view:I

    invoke-static {p1, v0, p0}, Landroid/view/View;->inflate(Landroid/content/Context;ILandroid/view/ViewGroup;)Landroid/view/View;

    .line 35
    new-instance v0, Lcom/helpshift/support/widget/CSATDialog;

    invoke-direct {v0, p1}, Lcom/helpshift/support/widget/CSATDialog;-><init>(Landroid/content/Context;)V

    iput-object v0, p0, Lcom/helpshift/support/widget/CSATView;->csatDialog:Lcom/helpshift/support/widget/CSATDialog;

    return-void
.end method


# virtual methods
.method protected dismiss()V
    .locals 1

    const/16 v0, 0x8

    .line 58
    invoke-virtual {p0, v0}, Lcom/helpshift/support/widget/CSATView;->setVisibility(I)V

    const/4 v0, 0x0

    .line 59
    iput-object v0, p0, Lcom/helpshift/support/widget/CSATView;->csatDialog:Lcom/helpshift/support/widget/CSATDialog;

    return-void
.end method

.method protected getRatingBar()Landroid/widget/RatingBar;
    .locals 1

    .line 54
    iget-object v0, p0, Lcom/helpshift/support/widget/CSATView;->ratingBar:Landroid/widget/RatingBar;

    return-object v0
.end method

.method protected onFinishInflate()V
    .locals 2

    .line 40
    invoke-super {p0}, Landroid/widget/RelativeLayout;->onFinishInflate()V

    .line 41
    sget v0, Lcom/helpshift/R$id;->ratingBar:I

    invoke-virtual {p0, v0}, Lcom/helpshift/support/widget/CSATView;->findViewById(I)Landroid/view/View;

    move-result-object v0

    check-cast v0, Landroid/widget/RatingBar;

    iput-object v0, p0, Lcom/helpshift/support/widget/CSATView;->ratingBar:Landroid/widget/RatingBar;

    .line 42
    invoke-virtual {p0}, Lcom/helpshift/support/widget/CSATView;->getContext()Landroid/content/Context;

    move-result-object v0

    iget-object v1, p0, Lcom/helpshift/support/widget/CSATView;->ratingBar:Landroid/widget/RatingBar;

    invoke-virtual {v1}, Landroid/widget/RatingBar;->getProgressDrawable()Landroid/graphics/drawable/Drawable;

    move-result-object v1

    invoke-static {v0, v1}, Lcom/helpshift/support/util/Styles;->setAccentColor(Landroid/content/Context;Landroid/graphics/drawable/Drawable;)V

    .line 43
    iget-object v0, p0, Lcom/helpshift/support/widget/CSATView;->ratingBar:Landroid/widget/RatingBar;

    invoke-virtual {v0, p0}, Landroid/widget/RatingBar;->setOnRatingBarChangeListener(Landroid/widget/RatingBar$OnRatingBarChangeListener;)V

    return-void
.end method

.method public onRatingChanged(Landroid/widget/RatingBar;FZ)V
    .locals 0

    if-eqz p3, :cond_0

    .line 49
    iget-object p1, p0, Lcom/helpshift/support/widget/CSATView;->csatDialog:Lcom/helpshift/support/widget/CSATDialog;

    invoke-virtual {p1, p0}, Lcom/helpshift/support/widget/CSATDialog;->show(Lcom/helpshift/support/widget/CSATView;)V

    :cond_0
    return-void
.end method

.method protected sendCSATSurvey(FLjava/lang/String;)V
    .locals 1

    .line 63
    iget-object v0, p0, Lcom/helpshift/support/widget/CSATView;->csatListener:Lcom/helpshift/support/widget/CSATView$CSATListener;

    if-eqz v0, :cond_0

    .line 64
    iget-object v0, p0, Lcom/helpshift/support/widget/CSATView;->csatListener:Lcom/helpshift/support/widget/CSATView$CSATListener;

    invoke-static {p1}, Ljava/lang/Math;->round(F)I

    move-result p1

    invoke-interface {v0, p1, p2}, Lcom/helpshift/support/widget/CSATView$CSATListener;->sendCSATSurvey(ILjava/lang/String;)V

    :cond_0
    return-void
.end method

.method public setCSATListener(Lcom/helpshift/support/widget/CSATView$CSATListener;)V
    .locals 0

    .line 69
    iput-object p1, p0, Lcom/helpshift/support/widget/CSATView;->csatListener:Lcom/helpshift/support/widget/CSATView$CSATListener;

    return-void
.end method
