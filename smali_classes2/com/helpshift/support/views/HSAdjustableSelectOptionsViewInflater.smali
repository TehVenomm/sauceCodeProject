.class public Lcom/helpshift/support/views/HSAdjustableSelectOptionsViewInflater;
.super Ljava/lang/Object;
.source "HSAdjustableSelectOptionsViewInflater.java"


# instance fields
.field private containerView:Landroid/widget/LinearLayout;

.field private context:Landroid/content/Context;

.field private maxAvailableWidthScaleFactor:D

.field private optionColorId:I

.field private optionDrawableId:I

.field private optionSelectionListener:Landroid/view/View$OnClickListener;

.field private optionTextViewId:I

.field private options:Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/message/input/OptionInput$Option;",
            ">;"
        }
    .end annotation
.end field

.field private rightEndPadding:I

.field private selectOptionInflaterLayout:I


# direct methods
.method public constructor <init>(Landroid/content/Context;DILandroid/widget/LinearLayout;IIIILjava/util/List;Landroid/view/View$OnClickListener;)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroid/content/Context;",
            "DI",
            "Landroid/widget/LinearLayout;",
            "IIII",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/message/input/OptionInput$Option;",
            ">;",
            "Landroid/view/View$OnClickListener;",
            ")V"
        }
    .end annotation

    .line 50
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 51
    iput-object p1, p0, Lcom/helpshift/support/views/HSAdjustableSelectOptionsViewInflater;->context:Landroid/content/Context;

    .line 52
    iput-wide p2, p0, Lcom/helpshift/support/views/HSAdjustableSelectOptionsViewInflater;->maxAvailableWidthScaleFactor:D

    .line 53
    iput p4, p0, Lcom/helpshift/support/views/HSAdjustableSelectOptionsViewInflater;->rightEndPadding:I

    .line 54
    iput-object p5, p0, Lcom/helpshift/support/views/HSAdjustableSelectOptionsViewInflater;->containerView:Landroid/widget/LinearLayout;

    .line 55
    iput p6, p0, Lcom/helpshift/support/views/HSAdjustableSelectOptionsViewInflater;->selectOptionInflaterLayout:I

    .line 56
    iput p7, p0, Lcom/helpshift/support/views/HSAdjustableSelectOptionsViewInflater;->optionTextViewId:I

    .line 57
    iput p8, p0, Lcom/helpshift/support/views/HSAdjustableSelectOptionsViewInflater;->optionDrawableId:I

    .line 58
    iput p9, p0, Lcom/helpshift/support/views/HSAdjustableSelectOptionsViewInflater;->optionColorId:I

    .line 59
    iput-object p11, p0, Lcom/helpshift/support/views/HSAdjustableSelectOptionsViewInflater;->optionSelectionListener:Landroid/view/View$OnClickListener;

    .line 60
    iput-object p10, p0, Lcom/helpshift/support/views/HSAdjustableSelectOptionsViewInflater;->options:Ljava/util/List;

    return-void
.end method


# virtual methods
.method public inflate()V
    .locals 15

    .line 65
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    .line 66
    iget-object v1, p0, Lcom/helpshift/support/views/HSAdjustableSelectOptionsViewInflater;->context:Landroid/content/Context;

    invoke-virtual {v1}, Landroid/content/Context;->getResources()Landroid/content/res/Resources;

    move-result-object v1

    invoke-virtual {v1}, Landroid/content/res/Resources;->getDisplayMetrics()Landroid/util/DisplayMetrics;

    move-result-object v1

    .line 69
    iget v2, v1, Landroid/util/DisplayMetrics;->widthPixels:I

    int-to-double v2, v2

    iget-wide v4, p0, Lcom/helpshift/support/views/HSAdjustableSelectOptionsViewInflater;->maxAvailableWidthScaleFactor:D

    invoke-static {v2, v3}, Ljava/lang/Double;->isNaN(D)Z

    mul-double v2, v2, v4

    double-to-int v2, v2

    iget v3, p0, Lcom/helpshift/support/views/HSAdjustableSelectOptionsViewInflater;->rightEndPadding:I

    int-to-float v3, v3

    iget v1, v1, Landroid/util/DisplayMetrics;->density:F

    mul-float v3, v3, v1

    float-to-int v1, v3

    sub-int/2addr v2, v1

    .line 72
    iget-object v1, p0, Lcom/helpshift/support/views/HSAdjustableSelectOptionsViewInflater;->options:Ljava/util/List;

    invoke-interface {v1}, Ljava/util/List;->size()I

    move-result v1

    const/4 v3, 0x0

    const/4 v4, 0x0

    :goto_0
    if-ge v4, v1, :cond_4

    .line 79
    new-instance v5, Landroid/widget/LinearLayout;

    iget-object v6, p0, Lcom/helpshift/support/views/HSAdjustableSelectOptionsViewInflater;->context:Landroid/content/Context;

    invoke-direct {v5, v6}, Landroid/widget/LinearLayout;-><init>(Landroid/content/Context;)V

    .line 80
    invoke-virtual {v5, v3}, Landroid/widget/LinearLayout;->setOrientation(I)V

    .line 81
    new-instance v6, Landroid/widget/LinearLayout$LayoutParams;

    const/4 v7, -0x1

    const/4 v8, -0x2

    invoke-direct {v6, v7, v8}, Landroid/widget/LinearLayout$LayoutParams;-><init>(II)V

    invoke-virtual {v5, v6}, Landroid/widget/LinearLayout;->setLayoutParams(Landroid/view/ViewGroup$LayoutParams;)V

    const v6, 0x800005

    .line 83
    invoke-virtual {v5, v6}, Landroid/widget/LinearLayout;->setGravity(I)V

    .line 89
    :cond_0
    iget-object v6, p0, Lcom/helpshift/support/views/HSAdjustableSelectOptionsViewInflater;->context:Landroid/content/Context;

    invoke-static {v6}, Landroid/view/LayoutInflater;->from(Landroid/content/Context;)Landroid/view/LayoutInflater;

    move-result-object v6

    iget v7, p0, Lcom/helpshift/support/views/HSAdjustableSelectOptionsViewInflater;->selectOptionInflaterLayout:I

    const/4 v8, 0x0

    invoke-virtual {v6, v7, v8, v3}, Landroid/view/LayoutInflater;->inflate(ILandroid/view/ViewGroup;Z)Landroid/view/View;

    move-result-object v6

    .line 90
    iget v7, p0, Lcom/helpshift/support/views/HSAdjustableSelectOptionsViewInflater;->optionTextViewId:I

    invoke-virtual {v6, v7}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v7

    check-cast v7, Landroid/widget/TextView;

    .line 92
    invoke-virtual {v7}, Landroid/widget/TextView;->getPaddingLeft()I

    move-result v8

    .line 93
    invoke-virtual {v7}, Landroid/widget/TextView;->getPaddingTop()I

    move-result v9

    .line 94
    invoke-virtual {v7}, Landroid/widget/TextView;->getPaddingRight()I

    move-result v10

    .line 95
    invoke-virtual {v7}, Landroid/widget/TextView;->getPaddingBottom()I

    move-result v11

    .line 99
    iget-object v12, p0, Lcom/helpshift/support/views/HSAdjustableSelectOptionsViewInflater;->context:Landroid/content/Context;

    iget v13, p0, Lcom/helpshift/support/views/HSAdjustableSelectOptionsViewInflater;->optionDrawableId:I

    iget v14, p0, Lcom/helpshift/support/views/HSAdjustableSelectOptionsViewInflater;->optionColorId:I

    invoke-static {v12, v7, v13, v14}, Lcom/helpshift/util/Styles;->setDrawable(Landroid/content/Context;Landroid/view/View;II)V

    .line 100
    invoke-virtual {v7, v8, v9, v10, v11}, Landroid/widget/TextView;->setPadding(IIII)V

    .line 101
    invoke-virtual {v7, v2}, Landroid/widget/TextView;->setMaxWidth(I)V

    .line 103
    iget-object v8, p0, Lcom/helpshift/support/views/HSAdjustableSelectOptionsViewInflater;->options:Ljava/util/List;

    invoke-interface {v8, v4}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v8

    check-cast v8, Lcom/helpshift/conversation/activeconversation/message/input/OptionInput$Option;

    .line 104
    invoke-virtual {v7, v8}, Landroid/widget/TextView;->setTag(Ljava/lang/Object;)V

    .line 105
    iget-object v8, v8, Lcom/helpshift/conversation/activeconversation/message/input/OptionInput$Option;->title:Ljava/lang/String;

    invoke-virtual {v7, v8}, Landroid/widget/TextView;->setText(Ljava/lang/CharSequence;)V

    .line 106
    iget-object v8, p0, Lcom/helpshift/support/views/HSAdjustableSelectOptionsViewInflater;->optionSelectionListener:Landroid/view/View$OnClickListener;

    invoke-virtual {v7, v8}, Landroid/widget/TextView;->setOnClickListener(Landroid/view/View$OnClickListener;)V

    .line 109
    invoke-virtual {v5, v6}, Landroid/widget/LinearLayout;->addView(Landroid/view/View;)V

    .line 110
    invoke-virtual {v5, v3, v3}, Landroid/widget/LinearLayout;->measure(II)V

    .line 111
    invoke-virtual {v5}, Landroid/widget/LinearLayout;->getMeasuredWidth()I

    move-result v7

    if-le v7, v2, :cond_2

    .line 118
    invoke-virtual {v5}, Landroid/widget/LinearLayout;->getChildCount()I

    move-result v7

    const/4 v8, 0x1

    if-ne v7, v8, :cond_1

    add-int/lit8 v4, v4, 0x1

    goto :goto_1

    .line 123
    :cond_1
    invoke-virtual {v5, v6}, Landroid/widget/LinearLayout;->removeView(Landroid/view/View;)V

    .line 125
    :goto_1
    invoke-interface {v0, v5}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto/16 :goto_0

    :cond_2
    add-int/lit8 v6, v1, -0x1

    if-ne v4, v6, :cond_3

    .line 132
    invoke-interface {v0, v5}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    :cond_3
    add-int/lit8 v4, v4, 0x1

    if-lt v4, v1, :cond_0

    goto/16 :goto_0

    .line 146
    :cond_4
    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :goto_2
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_5

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Landroid/widget/LinearLayout;

    .line 147
    iget-object v2, p0, Lcom/helpshift/support/views/HSAdjustableSelectOptionsViewInflater;->containerView:Landroid/widget/LinearLayout;

    invoke-virtual {v2, v1}, Landroid/widget/LinearLayout;->addView(Landroid/view/View;)V

    goto :goto_2

    :cond_5
    return-void
.end method
