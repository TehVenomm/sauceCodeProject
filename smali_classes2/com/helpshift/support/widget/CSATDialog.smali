.class public Lcom/helpshift/support/widget/CSATDialog;
.super Landroid/app/Dialog;
.source "CSATDialog.java"

# interfaces
.implements Landroid/content/DialogInterface$OnShowListener;
.implements Landroid/content/DialogInterface$OnDismissListener;
.implements Landroid/view/View$OnClickListener;
.implements Landroid/widget/RatingBar$OnRatingBarChangeListener;


# instance fields
.field private addtionalFeedback:Landroid/widget/EditText;

.field private context:Landroid/content/Context;

.field private csatView:Lcom/helpshift/support/widget/CSATView;

.field private dialogRatingBar:Landroid/widget/RatingBar;

.field private likeStatus:Landroid/widget/TextView;

.field private rating:F

.field private submitted:Z


# direct methods
.method public constructor <init>(Landroid/content/Context;)V
    .locals 1

    .line 34
    invoke-direct {p0, p1}, Landroid/app/Dialog;-><init>(Landroid/content/Context;)V

    const/4 v0, 0x0

    .line 31
    iput-boolean v0, p0, Lcom/helpshift/support/widget/CSATDialog;->submitted:Z

    .line 35
    iput-object p1, p0, Lcom/helpshift/support/widget/CSATDialog;->context:Landroid/content/Context;

    return-void
.end method

.method private setDialogRatingAndDescription(F)V
    .locals 5

    .line 102
    iget-object v0, p0, Lcom/helpshift/support/widget/CSATDialog;->dialogRatingBar:Landroid/widget/RatingBar;

    invoke-virtual {v0, p1}, Landroid/widget/RatingBar;->setRating(F)V

    float-to-double v0, p1

    const-wide/high16 v2, 0x4010000000000000L    # 4.0

    cmpl-double v4, v0, v2

    if-lez v4, :cond_0

    .line 112
    iget-object v0, p0, Lcom/helpshift/support/widget/CSATDialog;->likeStatus:Landroid/widget/TextView;

    sget v1, Lcom/helpshift/R$string;->hs__csat_like_message:I

    invoke-virtual {v0, v1}, Landroid/widget/TextView;->setText(I)V

    goto :goto_0

    :cond_0
    const-wide/high16 v2, 0x4008000000000000L    # 3.0

    cmpl-double v4, v0, v2

    if-lez v4, :cond_1

    .line 115
    iget-object v0, p0, Lcom/helpshift/support/widget/CSATDialog;->likeStatus:Landroid/widget/TextView;

    sget v1, Lcom/helpshift/R$string;->hs__csat_liked_rating_message:I

    invoke-virtual {v0, v1}, Landroid/widget/TextView;->setText(I)V

    goto :goto_0

    :cond_1
    const-wide/high16 v2, 0x4000000000000000L    # 2.0

    cmpl-double v4, v0, v2

    if-lez v4, :cond_2

    .line 118
    iget-object v0, p0, Lcom/helpshift/support/widget/CSATDialog;->likeStatus:Landroid/widget/TextView;

    sget v1, Lcom/helpshift/R$string;->hs__csat_ok_rating_message:I

    invoke-virtual {v0, v1}, Landroid/widget/TextView;->setText(I)V

    goto :goto_0

    :cond_2
    const-wide/high16 v2, 0x3ff0000000000000L    # 1.0

    cmpl-double v4, v0, v2

    if-lez v4, :cond_3

    .line 121
    iget-object v0, p0, Lcom/helpshift/support/widget/CSATDialog;->likeStatus:Landroid/widget/TextView;

    sget v1, Lcom/helpshift/R$string;->hs__csat_disliked_rating_message:I

    invoke-virtual {v0, v1}, Landroid/widget/TextView;->setText(I)V

    goto :goto_0

    .line 124
    :cond_3
    iget-object v0, p0, Lcom/helpshift/support/widget/CSATDialog;->likeStatus:Landroid/widget/TextView;

    sget v1, Lcom/helpshift/R$string;->hs__csat_dislike_message:I

    invoke-virtual {v0, v1}, Landroid/widget/TextView;->setText(I)V

    .line 127
    :goto_0
    iget-object v0, p0, Lcom/helpshift/support/widget/CSATDialog;->context:Landroid/content/Context;

    .line 128
    invoke-virtual {v0}, Landroid/content/Context;->getResources()Landroid/content/res/Resources;

    move-result-object v0

    sget v1, Lcom/helpshift/R$plurals;->hs__csat_rating_value:I

    float-to-int p1, p1

    const/4 v2, 0x1

    new-array v2, v2, [Ljava/lang/Object;

    const/4 v3, 0x0

    invoke-static {p1}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v4

    aput-object v4, v2, v3

    invoke-virtual {v0, v1, p1, v2}, Landroid/content/res/Resources;->getQuantityString(II[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object p1

    .line 129
    iget-object v0, p0, Lcom/helpshift/support/widget/CSATDialog;->dialogRatingBar:Landroid/widget/RatingBar;

    invoke-virtual {v0, p1}, Landroid/widget/RatingBar;->setContentDescription(Ljava/lang/CharSequence;)V

    return-void
.end method


# virtual methods
.method public onClick(Landroid/view/View;)V
    .locals 2

    .line 77
    invoke-virtual {p1}, Landroid/view/View;->getId()I

    move-result p1

    .line 78
    sget v0, Lcom/helpshift/R$id;->submit:I

    if-ne p1, v0, :cond_0

    .line 79
    iget-object p1, p0, Lcom/helpshift/support/widget/CSATDialog;->csatView:Lcom/helpshift/support/widget/CSATView;

    iget-object v0, p0, Lcom/helpshift/support/widget/CSATDialog;->dialogRatingBar:Landroid/widget/RatingBar;

    invoke-virtual {v0}, Landroid/widget/RatingBar;->getRating()F

    move-result v0

    iget-object v1, p0, Lcom/helpshift/support/widget/CSATDialog;->addtionalFeedback:Landroid/widget/EditText;

    invoke-virtual {v1}, Landroid/widget/EditText;->getText()Landroid/text/Editable;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {p1, v0, v1}, Lcom/helpshift/support/widget/CSATView;->sendCSATSurvey(FLjava/lang/String;)V

    const/4 p1, 0x1

    .line 80
    iput-boolean p1, p0, Lcom/helpshift/support/widget/CSATDialog;->submitted:Z

    .line 81
    invoke-virtual {p0}, Lcom/helpshift/support/widget/CSATDialog;->dismiss()V

    :cond_0
    return-void
.end method

.method protected onCreate(Landroid/os/Bundle;)V
    .locals 1

    const/4 v0, 0x1

    .line 40
    invoke-virtual {p0, v0}, Lcom/helpshift/support/widget/CSATDialog;->requestWindowFeature(I)Z

    .line 41
    invoke-super {p0, p1}, Landroid/app/Dialog;->onCreate(Landroid/os/Bundle;)V

    .line 42
    sget p1, Lcom/helpshift/R$layout;->hs__csat_dialog:I

    invoke-virtual {p0, p1}, Lcom/helpshift/support/widget/CSATDialog;->setContentView(I)V

    .line 43
    invoke-virtual {p0, p0}, Lcom/helpshift/support/widget/CSATDialog;->setOnShowListener(Landroid/content/DialogInterface$OnShowListener;)V

    .line 44
    invoke-virtual {p0, p0}, Lcom/helpshift/support/widget/CSATDialog;->setOnDismissListener(Landroid/content/DialogInterface$OnDismissListener;)V

    .line 46
    sget p1, Lcom/helpshift/R$id;->ratingBar:I

    invoke-virtual {p0, p1}, Lcom/helpshift/support/widget/CSATDialog;->findViewById(I)Landroid/view/View;

    move-result-object p1

    check-cast p1, Landroid/widget/RatingBar;

    iput-object p1, p0, Lcom/helpshift/support/widget/CSATDialog;->dialogRatingBar:Landroid/widget/RatingBar;

    .line 47
    invoke-virtual {p0}, Lcom/helpshift/support/widget/CSATDialog;->getContext()Landroid/content/Context;

    move-result-object p1

    iget-object v0, p0, Lcom/helpshift/support/widget/CSATDialog;->dialogRatingBar:Landroid/widget/RatingBar;

    invoke-virtual {v0}, Landroid/widget/RatingBar;->getProgressDrawable()Landroid/graphics/drawable/Drawable;

    move-result-object v0

    invoke-static {p1, v0}, Lcom/helpshift/support/util/Styles;->setAccentColor(Landroid/content/Context;Landroid/graphics/drawable/Drawable;)V

    .line 48
    iget-object p1, p0, Lcom/helpshift/support/widget/CSATDialog;->dialogRatingBar:Landroid/widget/RatingBar;

    invoke-virtual {p1, p0}, Landroid/widget/RatingBar;->setOnRatingBarChangeListener(Landroid/widget/RatingBar$OnRatingBarChangeListener;)V

    .line 50
    sget p1, Lcom/helpshift/R$id;->like_status:I

    invoke-virtual {p0, p1}, Lcom/helpshift/support/widget/CSATDialog;->findViewById(I)Landroid/view/View;

    move-result-object p1

    check-cast p1, Landroid/widget/TextView;

    iput-object p1, p0, Lcom/helpshift/support/widget/CSATDialog;->likeStatus:Landroid/widget/TextView;

    .line 51
    sget p1, Lcom/helpshift/R$id;->additional_feedback:I

    invoke-virtual {p0, p1}, Lcom/helpshift/support/widget/CSATDialog;->findViewById(I)Landroid/view/View;

    move-result-object p1

    check-cast p1, Landroid/widget/EditText;

    iput-object p1, p0, Lcom/helpshift/support/widget/CSATDialog;->addtionalFeedback:Landroid/widget/EditText;

    .line 53
    sget p1, Lcom/helpshift/R$id;->submit:I

    invoke-virtual {p0, p1}, Lcom/helpshift/support/widget/CSATDialog;->findViewById(I)Landroid/view/View;

    move-result-object p1

    check-cast p1, Landroid/widget/Button;

    .line 54
    invoke-virtual {p1, p0}, Landroid/widget/Button;->setOnClickListener(Landroid/view/View$OnClickListener;)V

    return-void
.end method

.method public onDismiss(Landroid/content/DialogInterface;)V
    .locals 1

    .line 66
    iget-boolean p1, p0, Lcom/helpshift/support/widget/CSATDialog;->submitted:Z

    if-eqz p1, :cond_0

    .line 67
    iget-object p1, p0, Lcom/helpshift/support/widget/CSATDialog;->csatView:Lcom/helpshift/support/widget/CSATView;

    invoke-virtual {p1}, Lcom/helpshift/support/widget/CSATView;->dismiss()V

    goto :goto_0

    .line 70
    :cond_0
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getCoreApi()Lcom/helpshift/CoreApi;

    move-result-object p1

    invoke-interface {p1}, Lcom/helpshift/CoreApi;->getAnalyticsEventDM()Lcom/helpshift/analytics/domainmodel/AnalyticsEventDM;

    move-result-object p1

    sget-object v0, Lcom/helpshift/analytics/AnalyticsEventType;->CANCEL_CSAT_RATING:Lcom/helpshift/analytics/AnalyticsEventType;

    invoke-virtual {p1, v0}, Lcom/helpshift/analytics/domainmodel/AnalyticsEventDM;->pushEvent(Lcom/helpshift/analytics/AnalyticsEventType;)V

    .line 71
    iget-object p1, p0, Lcom/helpshift/support/widget/CSATDialog;->csatView:Lcom/helpshift/support/widget/CSATView;

    invoke-virtual {p1}, Lcom/helpshift/support/widget/CSATView;->getRatingBar()Landroid/widget/RatingBar;

    move-result-object p1

    const/4 v0, 0x0

    invoke-virtual {p1, v0}, Landroid/widget/RatingBar;->setRating(F)V

    :goto_0
    return-void
.end method

.method public onRatingChanged(Landroid/widget/RatingBar;FZ)V
    .locals 0

    if-eqz p3, :cond_1

    const/high16 p1, 0x3f800000    # 1.0f

    cmpg-float p3, p2, p1

    if-gez p3, :cond_0

    goto :goto_0

    :cond_0
    move p1, p2

    .line 97
    :goto_0
    invoke-direct {p0, p1}, Lcom/helpshift/support/widget/CSATDialog;->setDialogRatingAndDescription(F)V

    :cond_1
    return-void
.end method

.method public onShow(Landroid/content/DialogInterface;)V
    .locals 1

    .line 59
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getCoreApi()Lcom/helpshift/CoreApi;

    move-result-object p1

    invoke-interface {p1}, Lcom/helpshift/CoreApi;->getAnalyticsEventDM()Lcom/helpshift/analytics/domainmodel/AnalyticsEventDM;

    move-result-object p1

    sget-object v0, Lcom/helpshift/analytics/AnalyticsEventType;->START_CSAT_RATING:Lcom/helpshift/analytics/AnalyticsEventType;

    invoke-virtual {p1, v0}, Lcom/helpshift/analytics/domainmodel/AnalyticsEventDM;->pushEvent(Lcom/helpshift/analytics/AnalyticsEventType;)V

    .line 61
    iget p1, p0, Lcom/helpshift/support/widget/CSATDialog;->rating:F

    invoke-direct {p0, p1}, Lcom/helpshift/support/widget/CSATDialog;->setDialogRatingAndDescription(F)V

    return-void
.end method

.method protected show(Lcom/helpshift/support/widget/CSATView;)V
    .locals 0

    .line 86
    iput-object p1, p0, Lcom/helpshift/support/widget/CSATDialog;->csatView:Lcom/helpshift/support/widget/CSATView;

    .line 87
    invoke-virtual {p1}, Lcom/helpshift/support/widget/CSATView;->getRatingBar()Landroid/widget/RatingBar;

    move-result-object p1

    invoke-virtual {p1}, Landroid/widget/RatingBar;->getRating()F

    move-result p1

    iput p1, p0, Lcom/helpshift/support/widget/CSATDialog;->rating:F

    .line 88
    invoke-virtual {p0}, Lcom/helpshift/support/widget/CSATDialog;->show()V

    return-void
.end method
