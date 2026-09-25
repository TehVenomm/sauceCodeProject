.class public Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;
.super Ljava/lang/Object;
.source "ConversationalFragmentRenderer.java"

# interfaces
.implements Lcom/helpshift/conversation/activeconversation/ConversationalRenderer;


# static fields
.field private static final OPTIONS_PICKER_PEEK_HEIGHT:I = 0x8e


# instance fields
.field bottomSheetBehavior:Lcom/google/android/material/bottomsheet/BottomSheetBehavior;

.field confirmationBoxView:Landroid/view/View;

.field context:Landroid/content/Context;

.field conversationalFragmentRouter:Lcom/helpshift/support/conversations/ConversationalFragmentRouter;

.field lastMessageItemDecor:Landroidx/recyclerview/widget/RecyclerView$ItemDecoration;

.field listPickerHostWindow:Landroid/view/Window;

.field menuItemRenderer:Lcom/helpshift/support/fragments/IToolbarMenuItemRenderer;

.field messagesAdapter:Lcom/helpshift/support/conversations/MessagesAdapter;

.field messagesRecyclerView:Landroidx/recyclerview/widget/RecyclerView;

.field networkErrorFooter:Landroid/widget/LinearLayout;

.field parentView:Landroid/view/View;

.field pickerAdapter:Lcom/helpshift/support/conversations/picker/PickerAdapter;

.field pickerBackView:Landroid/widget/ImageView;

.field pickerBottomSheet:Lcom/helpshift/views/bottomsheet/HSBottomSheet;

.field pickerClearView:Landroid/widget/ImageView;

.field pickerCollapseView:Landroid/widget/ImageView;

.field pickerCollapsedHeader:Landroid/view/View;

.field pickerCollapsedHeaderText:Landroid/widget/TextView;

.field pickerCollapsedShadow:Landroid/view/View;

.field pickerEmptySearchResultsView:Landroid/view/View;

.field pickerExpandView:Landroid/widget/ImageView;

.field pickerExpandedHeader:Landroid/view/View;

.field pickerExpandedHeaderText:Landroid/widget/TextView;

.field pickerExpandedShadow:Landroid/view/View;

.field pickerHeaderSearchView:Landroid/widget/EditText;

.field pickerOptionsRecycler:Landroidx/recyclerview/widget/RecyclerView;

.field pickerSearchView:Landroid/widget/ImageView;

.field replyBoxView:Landroid/view/View;

.field replyButton:Landroid/widget/ImageButton;

.field replyField:Landroid/widget/EditText;

.field replyValidationFailedView:Landroid/widget/TextView;

.field scrollIndicator:Landroid/view/View;

.field scrollJumpButton:Landroid/view/View;

.field skipBubbleTextView:Landroid/widget/TextView;

.field skipOutterBubble:Landroid/widget/LinearLayout;

.field unreadMessagesIndicatorDot:Landroid/view/View;


# direct methods
.method constructor <init>(Landroid/content/Context;Landroid/view/Window;Landroidx/recyclerview/widget/RecyclerView;Landroid/view/View;Landroid/view/View;Landroid/view/View;Landroid/view/View;Lcom/helpshift/support/fragments/IToolbarMenuItemRenderer;Lcom/helpshift/support/conversations/ConversationalFragmentRouter;)V
    .locals 0

    .line 132
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 133
    iput-object p1, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->context:Landroid/content/Context;

    .line 134
    iput-object p2, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->listPickerHostWindow:Landroid/view/Window;

    .line 135
    iput-object p3, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->messagesRecyclerView:Landroidx/recyclerview/widget/RecyclerView;

    .line 136
    iget-object p2, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->messagesRecyclerView:Landroidx/recyclerview/widget/RecyclerView;

    invoke-virtual {p2}, Landroidx/recyclerview/widget/RecyclerView;->getItemAnimator()Landroidx/recyclerview/widget/RecyclerView$ItemAnimator;

    move-result-object p2

    .line 137
    instance-of p3, p2, Landroidx/recyclerview/widget/SimpleItemAnimator;

    if-eqz p3, :cond_0

    .line 139
    check-cast p2, Landroidx/recyclerview/widget/SimpleItemAnimator;

    const/4 p3, 0x0

    invoke-virtual {p2, p3}, Landroidx/recyclerview/widget/SimpleItemAnimator;->setSupportsChangeAnimations(Z)V

    .line 141
    :cond_0
    iput-object p4, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->parentView:Landroid/view/View;

    .line 142
    sget p2, Lcom/helpshift/R$id;->replyBoxLayout:I

    invoke-virtual {p4, p2}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p2

    iput-object p2, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->replyBoxView:Landroid/view/View;

    .line 143
    iget-object p2, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->replyBoxView:Landroid/view/View;

    sget p3, Lcom/helpshift/R$id;->hs__messageText:I

    invoke-virtual {p2, p3}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p2

    check-cast p2, Landroid/widget/EditText;

    iput-object p2, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->replyField:Landroid/widget/EditText;

    .line 144
    iget-object p2, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->replyBoxView:Landroid/view/View;

    sget p3, Lcom/helpshift/R$id;->hs__sendMessageBtn:I

    invoke-virtual {p2, p3}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p2

    check-cast p2, Landroid/widget/ImageButton;

    iput-object p2, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->replyButton:Landroid/widget/ImageButton;

    .line 145
    sget p2, Lcom/helpshift/R$attr;->hs__messageSendIcon:I

    invoke-static {p1, p2}, Lcom/helpshift/support/util/Styles;->getResourceIdForAttribute(Landroid/content/Context;I)I

    move-result p2

    .line 150
    iget-object p3, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->replyButton:Landroid/widget/ImageButton;

    invoke-virtual {p1}, Landroid/content/Context;->getResources()Landroid/content/res/Resources;

    move-result-object p1

    invoke-virtual {p1, p2}, Landroid/content/res/Resources;->getDrawable(I)Landroid/graphics/drawable/Drawable;

    move-result-object p1

    invoke-virtual {p1}, Landroid/graphics/drawable/Drawable;->mutate()Landroid/graphics/drawable/Drawable;

    move-result-object p1

    invoke-virtual {p3, p1}, Landroid/widget/ImageButton;->setImageDrawable(Landroid/graphics/drawable/Drawable;)V

    .line 151
    sget p1, Lcom/helpshift/R$id;->scroll_jump_button:I

    invoke-virtual {p4, p1}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->scrollJumpButton:Landroid/view/View;

    .line 153
    iput-object p5, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->confirmationBoxView:Landroid/view/View;

    .line 154
    iput-object p9, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->conversationalFragmentRouter:Lcom/helpshift/support/conversations/ConversationalFragmentRouter;

    .line 155
    iput-object p8, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->menuItemRenderer:Lcom/helpshift/support/fragments/IToolbarMenuItemRenderer;

    .line 156
    iput-object p6, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->scrollIndicator:Landroid/view/View;

    .line 157
    iput-object p7, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->unreadMessagesIndicatorDot:Landroid/view/View;

    .line 158
    sget p1, Lcom/helpshift/R$id;->skipBubbleTextView:I

    invoke-virtual {p4, p1}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p1

    check-cast p1, Landroid/widget/TextView;

    iput-object p1, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->skipBubbleTextView:Landroid/widget/TextView;

    .line 159
    sget p1, Lcom/helpshift/R$id;->skipOuterBubble:I

    invoke-virtual {p4, p1}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p1

    check-cast p1, Landroid/widget/LinearLayout;

    iput-object p1, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->skipOutterBubble:Landroid/widget/LinearLayout;

    .line 160
    sget p1, Lcom/helpshift/R$id;->errorReplyTextView:I

    invoke-virtual {p4, p1}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p1

    check-cast p1, Landroid/widget/TextView;

    iput-object p1, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->replyValidationFailedView:Landroid/widget/TextView;

    .line 161
    sget p1, Lcom/helpshift/R$id;->networkErrorFooter:I

    invoke-virtual {p4, p1}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p1

    check-cast p1, Landroid/widget/LinearLayout;

    iput-object p1, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->networkErrorFooter:Landroid/widget/LinearLayout;

    .line 162
    iput-object p9, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->conversationalFragmentRouter:Lcom/helpshift/support/conversations/ConversationalFragmentRouter;

    return-void
.end method

.method static synthetic access$000(Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;)V
    .locals 0

    .line 83
    invoke-direct {p0}, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->onOptionPickerCollapsed()V

    return-void
.end method

.method static synthetic access$100(Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;)V
    .locals 0

    .line 83
    invoke-direct {p0}, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->onOptionPickerExpanded()V

    return-void
.end method

.method private calculatePickerBottomOffset(Z)I
    .locals 2

    const/16 v0, 0xe

    if-eqz p1, :cond_0

    .line 444
    iget-object p1, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->parentView:Landroid/view/View;

    invoke-virtual {p1}, Landroid/view/View;->getResources()Landroid/content/res/Resources;

    move-result-object p1

    sget v1, Lcom/helpshift/R$dimen;->activity_horizontal_margin_large:I

    invoke-virtual {p1, v1}, Landroid/content/res/Resources;->getDimension(I)F

    move-result p1

    float-to-int p1, p1

    add-int/2addr p1, v0

    add-int/lit8 p1, p1, 0x4

    .line 450
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->parentView:Landroid/view/View;

    sget v1, Lcom/helpshift/R$id;->hs__conversation_cardview_container:I

    invoke-virtual {v0, v1}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v0

    check-cast v0, Landroidx/cardview/widget/CardView;

    int-to-float p1, p1

    .line 451
    invoke-virtual {v0}, Landroidx/cardview/widget/CardView;->getCardElevation()F

    move-result v0

    add-float/2addr p1, v0

    float-to-int v0, p1

    :cond_0
    return v0
.end method

.method private changeMenuItemVisibility(Lcom/helpshift/support/fragments/HSMenuItemType;Z)V
    .locals 1

    .line 1278
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->menuItemRenderer:Lcom/helpshift/support/fragments/IToolbarMenuItemRenderer;

    if-eqz v0, :cond_0

    .line 1279
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->menuItemRenderer:Lcom/helpshift/support/fragments/IToolbarMenuItemRenderer;

    invoke-interface {v0, p1, p2}, Lcom/helpshift/support/fragments/IToolbarMenuItemRenderer;->updateMenuItemVisibility(Lcom/helpshift/support/fragments/HSMenuItemType;Z)V

    :cond_0
    return-void
.end method

.method private createRecyclerViewLastItemDecor()V
    .locals 1

    .line 782
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->lastMessageItemDecor:Landroidx/recyclerview/widget/RecyclerView$ItemDecoration;

    if-eqz v0, :cond_0

    return-void

    .line 787
    :cond_0
    new-instance v0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer$13;

    invoke-direct {v0, p0}, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer$13;-><init>(Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;)V

    iput-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->lastMessageItemDecor:Landroidx/recyclerview/widget/RecyclerView$ItemDecoration;

    return-void
.end method

.method private handleSkipButtonRenderingForPicker(ZLjava/lang/String;)V
    .locals 0

    if-nez p1, :cond_0

    .line 459
    invoke-static {p2}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result p1

    if-nez p1, :cond_0

    .line 460
    invoke-direct {p0}, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->setPickerOptionsInputSkipListener()V

    .line 461
    iget-object p1, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->skipBubbleTextView:Landroid/widget/TextView;

    invoke-virtual {p1, p2}, Landroid/widget/TextView;->setText(Ljava/lang/CharSequence;)V

    .line 462
    invoke-virtual {p0}, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->showSkipButton()V

    goto :goto_0

    .line 465
    :cond_0
    invoke-virtual {p0}, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->hideSkipButton()V

    :goto_0
    return-void
.end method

.method private hideScrollJumperView()V
    .locals 2

    .line 1255
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->scrollIndicator:Landroid/view/View;

    const/16 v1, 0x8

    invoke-virtual {v0, v1}, Landroid/view/View;->setVisibility(I)V

    .line 1256
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->unreadMessagesIndicatorDot:Landroid/view/View;

    invoke-virtual {v0, v1}, Landroid/view/View;->setVisibility(I)V

    return-void
.end method

.method private initBottomSheetCallback()V
    .locals 2

    .line 470
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->pickerBottomSheet:Lcom/helpshift/views/bottomsheet/HSBottomSheet;

    new-instance v1, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer$3;

    invoke-direct {v1, p0}, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer$3;-><init>(Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;)V

    invoke-virtual {v0, v1}, Lcom/helpshift/views/bottomsheet/HSBottomSheet;->addBottomSheetCallback(Lcom/google/android/material/bottomsheet/BottomSheetBehavior$BottomSheetCallback;)V

    return-void
.end method

.method private initPickerViews(Ljava/lang/String;)V
    .locals 6

    .line 658
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->pickerBottomSheet:Lcom/helpshift/views/bottomsheet/HSBottomSheet;

    invoke-virtual {v0}, Lcom/helpshift/views/bottomsheet/HSBottomSheet;->getBottomSheetBehaviour()Lcom/google/android/material/bottomsheet/BottomSheetBehavior;

    move-result-object v0

    iput-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->bottomSheetBehavior:Lcom/google/android/material/bottomsheet/BottomSheetBehavior;

    .line 659
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->pickerBottomSheet:Lcom/helpshift/views/bottomsheet/HSBottomSheet;

    invoke-virtual {v0}, Lcom/helpshift/views/bottomsheet/HSBottomSheet;->getBottomSheetContentView()Landroid/view/View;

    move-result-object v0

    .line 660
    sget v1, Lcom/helpshift/R$id;->hs__picker_collapsed_shadow:I

    invoke-virtual {v0, v1}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v1

    iput-object v1, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->pickerCollapsedShadow:Landroid/view/View;

    .line 661
    sget v1, Lcom/helpshift/R$id;->hs__picker_expanded_shadow:I

    invoke-virtual {v0, v1}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v1

    iput-object v1, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->pickerExpandedShadow:Landroid/view/View;

    .line 662
    sget v1, Lcom/helpshift/R$id;->hs__optionsList:I

    invoke-virtual {v0, v1}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v1

    check-cast v1, Landroidx/recyclerview/widget/RecyclerView;

    iput-object v1, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->pickerOptionsRecycler:Landroidx/recyclerview/widget/RecyclerView;

    .line 663
    iget-object v1, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->pickerOptionsRecycler:Landroidx/recyclerview/widget/RecyclerView;

    new-instance v2, Landroidx/recyclerview/widget/LinearLayoutManager;

    .line 665
    invoke-virtual {v0}, Landroid/view/View;->getContext()Landroid/content/Context;

    move-result-object v3

    const/4 v4, 0x0

    const/4 v5, 0x1

    invoke-direct {v2, v3, v5, v4}, Landroidx/recyclerview/widget/LinearLayoutManager;-><init>(Landroid/content/Context;IZ)V

    .line 664
    invoke-virtual {v1, v2}, Landroidx/recyclerview/widget/RecyclerView;->setLayoutManager(Landroidx/recyclerview/widget/RecyclerView$LayoutManager;)V

    .line 666
    sget v1, Lcom/helpshift/R$id;->hs__picker_action_search:I

    invoke-virtual {v0, v1}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v1

    check-cast v1, Landroid/widget/ImageView;

    iput-object v1, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->pickerSearchView:Landroid/widget/ImageView;

    .line 667
    sget v1, Lcom/helpshift/R$id;->hs__picker_action_clear:I

    invoke-virtual {v0, v1}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v1

    check-cast v1, Landroid/widget/ImageView;

    iput-object v1, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->pickerClearView:Landroid/widget/ImageView;

    .line 668
    sget v1, Lcom/helpshift/R$id;->hs__picker_action_collapse:I

    invoke-virtual {v0, v1}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v1

    check-cast v1, Landroid/widget/ImageView;

    iput-object v1, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->pickerCollapseView:Landroid/widget/ImageView;

    .line 669
    sget v1, Lcom/helpshift/R$id;->hs__picker_action_back:I

    invoke-virtual {v0, v1}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v1

    check-cast v1, Landroid/widget/ImageView;

    iput-object v1, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->pickerBackView:Landroid/widget/ImageView;

    .line 670
    sget v1, Lcom/helpshift/R$id;->hs__picker_header_search:I

    invoke-virtual {v0, v1}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v1

    check-cast v1, Landroid/widget/EditText;

    iput-object v1, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->pickerHeaderSearchView:Landroid/widget/EditText;

    .line 671
    sget v1, Lcom/helpshift/R$id;->hs__expanded_picker_header_text:I

    invoke-virtual {v0, v1}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v1

    check-cast v1, Landroid/widget/TextView;

    iput-object v1, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->pickerExpandedHeaderText:Landroid/widget/TextView;

    .line 672
    sget v1, Lcom/helpshift/R$id;->hs__picker_expanded_header:I

    invoke-virtual {v0, v1}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v1

    iput-object v1, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->pickerExpandedHeader:Landroid/view/View;

    .line 673
    sget v1, Lcom/helpshift/R$id;->hs__picker_collapsed_header:I

    invoke-virtual {v0, v1}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v1

    iput-object v1, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->pickerCollapsedHeader:Landroid/view/View;

    .line 674
    sget v1, Lcom/helpshift/R$id;->hs__collapsed_picker_header_text:I

    invoke-virtual {v0, v1}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v1

    check-cast v1, Landroid/widget/TextView;

    iput-object v1, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->pickerCollapsedHeaderText:Landroid/widget/TextView;

    .line 675
    sget v1, Lcom/helpshift/R$id;->hs__empty_picker_view:I

    invoke-virtual {v0, v1}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v1

    iput-object v1, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->pickerEmptySearchResultsView:Landroid/view/View;

    .line 676
    sget v1, Lcom/helpshift/R$id;->hs__picker_action_expand:I

    invoke-virtual {v0, v1}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v0

    check-cast v0, Landroid/widget/ImageView;

    iput-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->pickerExpandView:Landroid/widget/ImageView;

    .line 678
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->pickerExpandedHeaderText:Landroid/widget/TextView;

    invoke-virtual {v0, p1}, Landroid/widget/TextView;->setText(Ljava/lang/CharSequence;)V

    .line 679
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->pickerCollapsedHeaderText:Landroid/widget/TextView;

    invoke-virtual {v0, p1}, Landroid/widget/TextView;->setText(Ljava/lang/CharSequence;)V

    .line 682
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->parentView:Landroid/view/View;

    .line 683
    invoke-virtual {v0}, Landroid/view/View;->getResources()Landroid/content/res/Resources;

    move-result-object v0

    sget v1, Lcom/helpshift/R$string;->hs__picker_options_expand_header_voice_over:I

    new-array v2, v5, [Ljava/lang/Object;

    aput-object p1, v2, v4

    invoke-virtual {v0, v1, v2}, Landroid/content/res/Resources;->getString(I[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object p1

    .line 685
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->pickerCollapsedHeader:Landroid/view/View;

    invoke-virtual {v0, p1}, Landroid/view/View;->setContentDescription(Ljava/lang/CharSequence;)V

    .line 686
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->pickerCollapsedHeaderText:Landroid/widget/TextView;

    invoke-virtual {v0, p1}, Landroid/widget/TextView;->setContentDescription(Ljava/lang/CharSequence;)V

    .line 689
    iget-object p1, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->context:Landroid/content/Context;

    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->pickerSearchView:Landroid/widget/ImageView;

    invoke-virtual {v0}, Landroid/widget/ImageView;->getDrawable()Landroid/graphics/drawable/Drawable;

    move-result-object v0

    sget v1, Lcom/helpshift/R$attr;->hs__expandedPickerIconColor:I

    invoke-static {p1, v0, v1}, Lcom/helpshift/util/Styles;->setColorFilter(Landroid/content/Context;Landroid/graphics/drawable/Drawable;I)V

    .line 690
    iget-object p1, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->context:Landroid/content/Context;

    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->pickerBackView:Landroid/widget/ImageView;

    invoke-virtual {v0}, Landroid/widget/ImageView;->getDrawable()Landroid/graphics/drawable/Drawable;

    move-result-object v0

    sget v1, Lcom/helpshift/R$attr;->hs__expandedPickerIconColor:I

    invoke-static {p1, v0, v1}, Lcom/helpshift/util/Styles;->setColorFilter(Landroid/content/Context;Landroid/graphics/drawable/Drawable;I)V

    .line 691
    iget-object p1, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->context:Landroid/content/Context;

    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->pickerCollapseView:Landroid/widget/ImageView;

    invoke-virtual {v0}, Landroid/widget/ImageView;->getDrawable()Landroid/graphics/drawable/Drawable;

    move-result-object v0

    sget v1, Lcom/helpshift/R$attr;->hs__expandedPickerIconColor:I

    invoke-static {p1, v0, v1}, Lcom/helpshift/util/Styles;->setColorFilter(Landroid/content/Context;Landroid/graphics/drawable/Drawable;I)V

    .line 692
    iget-object p1, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->context:Landroid/content/Context;

    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->pickerClearView:Landroid/widget/ImageView;

    invoke-virtual {v0}, Landroid/widget/ImageView;->getDrawable()Landroid/graphics/drawable/Drawable;

    move-result-object v0

    sget v1, Lcom/helpshift/R$attr;->hs__expandedPickerIconColor:I

    invoke-static {p1, v0, v1}, Lcom/helpshift/util/Styles;->setColorFilter(Landroid/content/Context;Landroid/graphics/drawable/Drawable;I)V

    .line 693
    iget-object p1, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->context:Landroid/content/Context;

    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->pickerExpandView:Landroid/widget/ImageView;

    invoke-virtual {v0}, Landroid/widget/ImageView;->getDrawable()Landroid/graphics/drawable/Drawable;

    move-result-object v0

    sget v1, Lcom/helpshift/R$attr;->hs__collapsedPickerIconColor:I

    invoke-static {p1, v0, v1}, Lcom/helpshift/util/Styles;->setColorFilter(Landroid/content/Context;Landroid/graphics/drawable/Drawable;I)V

    return-void
.end method

.method private launchAttachmentIntentInternal(Landroid/content/Intent;Landroid/net/Uri;)V
    .locals 1

    .line 1028
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->context:Landroid/content/Context;

    invoke-virtual {v0}, Landroid/content/Context;->getPackageManager()Landroid/content/pm/PackageManager;

    move-result-object v0

    invoke-virtual {p1, v0}, Landroid/content/Intent;->resolveActivity(Landroid/content/pm/PackageManager;)Landroid/content/ComponentName;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 1029
    iget-object p2, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->context:Landroid/content/Context;

    invoke-virtual {p2, p1}, Landroid/content/Context;->startActivity(Landroid/content/Intent;)V

    goto :goto_0

    .line 1031
    :cond_0
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getCoreApi()Lcom/helpshift/CoreApi;

    move-result-object p1

    invoke-interface {p1}, Lcom/helpshift/CoreApi;->getDelegate()Lcom/helpshift/delegate/UIThreadDelegateDecorator;

    move-result-object p1

    invoke-virtual {p1}, Lcom/helpshift/delegate/UIThreadDelegateDecorator;->isDelegateRegistered()Z

    move-result p1

    if-eqz p1, :cond_2

    .line 1037
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getCoreApi()Lcom/helpshift/CoreApi;

    move-result-object p1

    invoke-interface {p1}, Lcom/helpshift/CoreApi;->getDelegate()Lcom/helpshift/delegate/UIThreadDelegateDecorator;

    move-result-object p1

    .line 1038
    invoke-virtual {p1}, Lcom/helpshift/delegate/UIThreadDelegateDecorator;->getDelegate()Lcom/helpshift/delegate/RootDelegate;

    move-result-object p1

    .line 1039
    instance-of v0, p1, Lcom/helpshift/support/Support$Delegate;

    if-eqz v0, :cond_1

    .line 1040
    check-cast p1, Lcom/helpshift/support/Support$Delegate;

    .line 1041
    invoke-interface {p1, p2}, Lcom/helpshift/support/Support$Delegate;->displayAttachmentFile(Landroid/net/Uri;)V

    goto :goto_0

    .line 1044
    :cond_1
    sget-object p1, Lcom/helpshift/common/exception/PlatformException;->NO_APPS_FOR_OPENING_ATTACHMENT:Lcom/helpshift/common/exception/PlatformException;

    invoke-virtual {p0, p1}, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->showErrorView(Lcom/helpshift/common/exception/ExceptionType;)V

    goto :goto_0

    .line 1048
    :cond_2
    sget-object p1, Lcom/helpshift/common/exception/PlatformException;->NO_APPS_FOR_OPENING_ATTACHMENT:Lcom/helpshift/common/exception/PlatformException;

    invoke-virtual {p0, p1}, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->showErrorView(Lcom/helpshift/common/exception/ExceptionType;)V

    :goto_0
    return-void
.end method

.method private launchAttachmentIntentInternal(Landroid/content/Intent;Ljava/io/File;)V
    .locals 1

    .line 1056
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->context:Landroid/content/Context;

    invoke-virtual {v0}, Landroid/content/Context;->getPackageManager()Landroid/content/pm/PackageManager;

    move-result-object v0

    invoke-virtual {p1, v0}, Landroid/content/Intent;->resolveActivity(Landroid/content/pm/PackageManager;)Landroid/content/ComponentName;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 1057
    iget-object p2, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->context:Landroid/content/Context;

    invoke-virtual {p2, p1}, Landroid/content/Context;->startActivity(Landroid/content/Intent;)V

    goto :goto_0

    .line 1059
    :cond_0
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getCoreApi()Lcom/helpshift/CoreApi;

    move-result-object p1

    invoke-interface {p1}, Lcom/helpshift/CoreApi;->getDelegate()Lcom/helpshift/delegate/UIThreadDelegateDecorator;

    move-result-object p1

    invoke-virtual {p1}, Lcom/helpshift/delegate/UIThreadDelegateDecorator;->isDelegateRegistered()Z

    move-result p1

    if-eqz p1, :cond_1

    .line 1060
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getCoreApi()Lcom/helpshift/CoreApi;

    move-result-object p1

    invoke-interface {p1}, Lcom/helpshift/CoreApi;->getDelegate()Lcom/helpshift/delegate/UIThreadDelegateDecorator;

    move-result-object p1

    invoke-virtual {p1, p2}, Lcom/helpshift/delegate/UIThreadDelegateDecorator;->displayAttachmentFile(Ljava/io/File;)V

    goto :goto_0

    .line 1063
    :cond_1
    sget-object p1, Lcom/helpshift/common/exception/PlatformException;->NO_APPS_FOR_OPENING_ATTACHMENT:Lcom/helpshift/common/exception/PlatformException;

    invoke-virtual {p0, p1}, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->showErrorView(Lcom/helpshift/common/exception/ExceptionType;)V

    :goto_0
    return-void
.end method

.method private onOptionPickerCollapsed()V
    .locals 4

    .line 544
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->pickerCollapsedShadow:Landroid/view/View;

    const/4 v1, 0x0

    invoke-virtual {v0, v1}, Landroid/view/View;->setVisibility(I)V

    .line 545
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->pickerCollapsedShadow:Landroid/view/View;

    iget-object v2, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->context:Landroid/content/Context;

    sget v3, Lcom/helpshift/R$color;->hs__color_40000000:I

    .line 546
    invoke-static {v2, v3}, Landroidx/core/content/ContextCompat;->getColor(Landroid/content/Context;I)I

    move-result v2

    sget-object v3, Landroid/graphics/drawable/GradientDrawable$Orientation;->BOTTOM_TOP:Landroid/graphics/drawable/GradientDrawable$Orientation;

    .line 545
    invoke-static {v0, v2, v1, v3}, Lcom/helpshift/util/Styles;->setGradientBackground(Landroid/view/View;IILandroid/graphics/drawable/GradientDrawable$Orientation;)V

    .line 552
    invoke-direct {p0}, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->showPickerContent()V

    .line 556
    invoke-virtual {p0}, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->resetPickerSearchViewToNormalHeader()V

    .line 558
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->pickerCollapsedHeader:Landroid/view/View;

    invoke-virtual {v0, v1}, Landroid/view/View;->setVisibility(I)V

    .line 559
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->pickerExpandedHeader:Landroid/view/View;

    const/16 v2, 0x8

    invoke-virtual {v0, v2}, Landroid/view/View;->setVisibility(I)V

    .line 562
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->pickerOptionsRecycler:Landroidx/recyclerview/widget/RecyclerView;

    invoke-virtual {v0, v1}, Landroidx/recyclerview/widget/RecyclerView;->scrollToPosition(I)V

    .line 564
    invoke-direct {p0}, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->resetAccessibilityForToolbar()V

    return-void
.end method

.method private onOptionPickerExpanded()V
    .locals 5

    .line 523
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->pickerCollapsedShadow:Landroid/view/View;

    const/16 v1, 0x8

    invoke-virtual {v0, v1}, Landroid/view/View;->setVisibility(I)V

    .line 524
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->pickerExpandedShadow:Landroid/view/View;

    iget-object v2, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->context:Landroid/content/Context;

    sget v3, Lcom/helpshift/R$color;->hs__color_40000000:I

    .line 525
    invoke-static {v2, v3}, Landroidx/core/content/ContextCompat;->getColor(Landroid/content/Context;I)I

    move-result v2

    sget-object v3, Landroid/graphics/drawable/GradientDrawable$Orientation;->TOP_BOTTOM:Landroid/graphics/drawable/GradientDrawable$Orientation;

    const/4 v4, 0x0

    .line 524
    invoke-static {v0, v2, v4, v3}, Lcom/helpshift/util/Styles;->setGradientBackground(Landroid/view/View;IILandroid/graphics/drawable/GradientDrawable$Orientation;)V

    .line 528
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->pickerExpandedHeader:Landroid/view/View;

    invoke-virtual {v0, v4}, Landroid/view/View;->setVisibility(I)V

    .line 529
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->pickerCollapsedHeader:Landroid/view/View;

    invoke-virtual {v0, v1}, Landroid/view/View;->setVisibility(I)V

    .line 534
    sget v0, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 v1, 0x13

    if-lt v0, v1, :cond_0

    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->parentView:Landroid/view/View;

    if-eqz v0, :cond_0

    .line 535
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->parentView:Landroid/view/View;

    const/4 v1, 0x4

    invoke-virtual {v0, v1}, Landroid/view/View;->setImportantForAccessibility(I)V

    .line 537
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->conversationalFragmentRouter:Lcom/helpshift/support/conversations/ConversationalFragmentRouter;

    .line 538
    invoke-interface {v0, v1}, Lcom/helpshift/support/conversations/ConversationalFragmentRouter;->setToolbarImportanceForAccessibility(I)V

    :cond_0
    return-void
.end method

.method private registerListeners()V
    .locals 2

    .line 579
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->pickerHeaderSearchView:Landroid/widget/EditText;

    new-instance v1, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer$4;

    invoke-direct {v1, p0}, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer$4;-><init>(Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;)V

    invoke-virtual {v0, v1}, Landroid/widget/EditText;->addTextChangedListener(Landroid/text/TextWatcher;)V

    .line 588
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->pickerHeaderSearchView:Landroid/widget/EditText;

    new-instance v1, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer$5;

    invoke-direct {v1, p0}, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer$5;-><init>(Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;)V

    invoke-virtual {v0, v1}, Landroid/widget/EditText;->setOnEditorActionListener(Landroid/widget/TextView$OnEditorActionListener;)V

    .line 599
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->pickerSearchView:Landroid/widget/ImageView;

    new-instance v1, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer$6;

    invoke-direct {v1, p0}, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer$6;-><init>(Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;)V

    invoke-virtual {v0, v1}, Landroid/widget/ImageView;->setOnClickListener(Landroid/view/View$OnClickListener;)V

    .line 614
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->pickerBackView:Landroid/widget/ImageView;

    new-instance v1, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer$7;

    invoke-direct {v1, p0}, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer$7;-><init>(Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;)V

    invoke-virtual {v0, v1}, Landroid/widget/ImageView;->setOnClickListener(Landroid/view/View$OnClickListener;)V

    .line 621
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->pickerClearView:Landroid/widget/ImageView;

    new-instance v1, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer$8;

    invoke-direct {v1, p0}, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer$8;-><init>(Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;)V

    invoke-virtual {v0, v1}, Landroid/widget/ImageView;->setOnClickListener(Landroid/view/View$OnClickListener;)V

    .line 629
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->pickerCollapseView:Landroid/widget/ImageView;

    new-instance v1, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer$9;

    invoke-direct {v1, p0}, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer$9;-><init>(Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;)V

    invoke-virtual {v0, v1}, Landroid/widget/ImageView;->setOnClickListener(Landroid/view/View$OnClickListener;)V

    .line 637
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->pickerCollapsedHeader:Landroid/view/View;

    new-instance v1, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer$10;

    invoke-direct {v1, p0}, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer$10;-><init>(Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;)V

    invoke-virtual {v0, v1}, Landroid/view/View;->setOnClickListener(Landroid/view/View$OnClickListener;)V

    return-void
.end method

.method private renderForTextInput(Lcom/helpshift/conversation/activeconversation/message/input/TextInput;)V
    .locals 3

    .line 847
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->replyField:Landroid/widget/EditText;

    const/4 v1, 0x1

    invoke-virtual {v0, v1}, Landroid/widget/EditText;->setFocusableInTouchMode(Z)V

    .line 848
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->replyField:Landroid/widget/EditText;

    const/4 v1, 0x0

    invoke-virtual {v0, v1}, Landroid/widget/EditText;->setOnClickListener(Landroid/view/View$OnClickListener;)V

    .line 851
    iget-object v0, p1, Lcom/helpshift/conversation/activeconversation/message/input/TextInput;->inputLabel:Ljava/lang/String;

    invoke-static {v0}, Landroid/text/TextUtils;->isEmpty(Ljava/lang/CharSequence;)Z

    move-result v0

    const/4 v1, 0x0

    if-nez v0, :cond_0

    .line 852
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->parentView:Landroid/view/View;

    sget v2, Lcom/helpshift/R$id;->replyBoxLabelLayout:I

    invoke-virtual {v0, v2}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v0

    check-cast v0, Landroid/widget/LinearLayout;

    .line 853
    invoke-virtual {v0, v1}, Landroid/widget/LinearLayout;->setVisibility(I)V

    .line 854
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->replyBoxView:Landroid/view/View;

    sget v2, Lcom/helpshift/R$id;->replyFieldLabel:I

    invoke-virtual {v0, v2}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v0

    check-cast v0, Landroid/widget/TextView;

    .line 855
    iget-object v2, p1, Lcom/helpshift/conversation/activeconversation/message/input/TextInput;->inputLabel:Ljava/lang/String;

    invoke-virtual {v0, v2}, Landroid/widget/TextView;->setText(Ljava/lang/CharSequence;)V

    .line 859
    :cond_0
    iget-object v0, p1, Lcom/helpshift/conversation/activeconversation/message/input/TextInput;->placeholder:Ljava/lang/String;

    invoke-static {v0}, Landroid/text/TextUtils;->isEmpty(Ljava/lang/CharSequence;)Z

    move-result v0

    if-eqz v0, :cond_1

    const-string v0, ""

    goto :goto_0

    :cond_1
    iget-object v0, p1, Lcom/helpshift/conversation/activeconversation/message/input/TextInput;->placeholder:Ljava/lang/String;

    .line 860
    :goto_0
    iget-object v2, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->replyField:Landroid/widget/EditText;

    invoke-virtual {v2, v0}, Landroid/widget/EditText;->setHint(Ljava/lang/CharSequence;)V

    const/high16 v0, 0x20000

    .line 864
    iget v2, p1, Lcom/helpshift/conversation/activeconversation/message/input/TextInput;->keyboard:I

    packed-switch v2, :pswitch_data_0

    .line 891
    invoke-direct {p0}, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->resetReplyFieldToNormalTextInput()V

    goto :goto_1

    .line 876
    :pswitch_0
    invoke-virtual {p0}, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->hideKeyboard()V

    .line 881
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->replyField:Landroid/widget/EditText;

    invoke-virtual {v0, v1}, Landroid/widget/EditText;->setFocusableInTouchMode(Z)V

    .line 882
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->replyField:Landroid/widget/EditText;

    new-instance v2, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer$15;

    invoke-direct {v2, p0}, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer$15;-><init>(Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;)V

    invoke-virtual {v0, v2}, Landroid/widget/EditText;->setOnClickListener(Landroid/view/View$OnClickListener;)V

    const/4 v0, 0x0

    goto :goto_1

    :pswitch_1
    const v0, 0x22002

    goto :goto_1

    :pswitch_2
    const v0, 0x20021

    goto :goto_1

    :pswitch_3
    const v0, 0x24001

    .line 895
    :goto_1
    iget-object v2, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->replyField:Landroid/widget/EditText;

    invoke-virtual {v2, v0}, Landroid/widget/EditText;->setInputType(I)V

    .line 898
    iget-boolean v0, p1, Lcom/helpshift/conversation/activeconversation/message/input/TextInput;->required:Z

    if-nez v0, :cond_2

    iget-object v0, p1, Lcom/helpshift/conversation/activeconversation/message/input/TextInput;->skipLabel:Ljava/lang/String;

    invoke-static {v0}, Landroid/text/TextUtils;->isEmpty(Ljava/lang/CharSequence;)Z

    move-result v0

    if-nez v0, :cond_2

    .line 900
    invoke-direct {p0}, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->setTextInputSkipListener()V

    .line 901
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->skipBubbleTextView:Landroid/widget/TextView;

    iget-object p1, p1, Lcom/helpshift/conversation/activeconversation/message/input/TextInput;->skipLabel:Ljava/lang/String;

    invoke-virtual {v0, p1}, Landroid/widget/TextView;->setText(Ljava/lang/CharSequence;)V

    .line 902
    invoke-virtual {p0}, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->showSkipButton()V

    goto :goto_2

    .line 905
    :cond_2
    invoke-virtual {p0}, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->hideSkipButton()V

    .line 908
    :goto_2
    iget-object p1, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->replyBoxView:Landroid/view/View;

    invoke-virtual {p1, v1}, Landroid/view/View;->setVisibility(I)V

    return-void

    nop

    :pswitch_data_0
    .packed-switch 0x1
        :pswitch_3
        :pswitch_2
        :pswitch_1
        :pswitch_0
    .end packed-switch
.end method

.method private resetAccessibilityForToolbar()V
    .locals 2

    .line 571
    sget v0, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 v1, 0x13

    if-lt v0, v1, :cond_0

    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->parentView:Landroid/view/View;

    if-eqz v0, :cond_0

    .line 572
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->parentView:Landroid/view/View;

    const/4 v1, 0x0

    invoke-virtual {v0, v1}, Landroid/view/View;->setImportantForAccessibility(I)V

    .line 574
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->conversationalFragmentRouter:Lcom/helpshift/support/conversations/ConversationalFragmentRouter;

    invoke-interface {v0}, Lcom/helpshift/support/conversations/ConversationalFragmentRouter;->resetToolbarImportanceForAccessibility()V

    :cond_0
    return-void
.end method

.method private resetReplyFieldToNormalTextInput()V
    .locals 2

    .line 700
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->replyField:Landroid/widget/EditText;

    const v1, 0x24001

    invoke-virtual {v0, v1}, Landroid/widget/EditText;->setInputType(I)V

    .line 703
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->replyField:Landroid/widget/EditText;

    sget v1, Lcom/helpshift/R$string;->hs__chat_hint:I

    invoke-virtual {v0, v1}, Landroid/widget/EditText;->setHint(I)V

    return-void
.end method

.method private setBottomOffset(Landroid/view/View;I)V
    .locals 4

    .line 355
    invoke-virtual {p1}, Landroid/view/View;->getPaddingLeft()I

    move-result v0

    .line 356
    invoke-virtual {p1}, Landroid/view/View;->getPaddingRight()I

    move-result v1

    .line 357
    invoke-virtual {p1}, Landroid/view/View;->getPaddingTop()I

    move-result v2

    .line 359
    iget-object v3, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->context:Landroid/content/Context;

    int-to-float p2, p2

    invoke-static {v3, p2}, Lcom/helpshift/util/Styles;->dpToPx(Landroid/content/Context;F)F

    move-result p2

    float-to-int p2, p2

    .line 360
    invoke-virtual {p1, v0, v2, v1, p2}, Landroid/view/View;->setPadding(IIII)V

    return-void
.end method

.method private setPickerOptionsInputSkipListener()V
    .locals 2

    .line 771
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->skipBubbleTextView:Landroid/widget/TextView;

    new-instance v1, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer$12;

    invoke-direct {v1, p0}, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer$12;-><init>(Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;)V

    invoke-virtual {v0, v1}, Landroid/widget/TextView;->setOnClickListener(Landroid/view/View$OnClickListener;)V

    return-void
.end method

.method private setTextInputSkipListener()V
    .locals 2

    .line 762
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->skipBubbleTextView:Landroid/widget/TextView;

    new-instance v1, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer$11;

    invoke-direct {v1, p0}, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer$11;-><init>(Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;)V

    invoke-virtual {v0, v1}, Landroid/widget/TextView;->setOnClickListener(Landroid/view/View$OnClickListener;)V

    return-void
.end method

.method private showEmptyPickerView()V
    .locals 2

    .line 380
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->pickerEmptySearchResultsView:Landroid/view/View;

    invoke-virtual {v0}, Landroid/view/View;->isShown()Z

    move-result v0

    if-nez v0, :cond_0

    .line 381
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->pickerEmptySearchResultsView:Landroid/view/View;

    const/4 v1, 0x0

    invoke-virtual {v0, v1}, Landroid/view/View;->setVisibility(I)V

    .line 384
    :cond_0
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->pickerOptionsRecycler:Landroidx/recyclerview/widget/RecyclerView;

    invoke-virtual {v0}, Landroidx/recyclerview/widget/RecyclerView;->isShown()Z

    move-result v0

    if-eqz v0, :cond_1

    .line 385
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->pickerOptionsRecycler:Landroidx/recyclerview/widget/RecyclerView;

    const/16 v1, 0x8

    invoke-virtual {v0, v1}, Landroidx/recyclerview/widget/RecyclerView;->setVisibility(I)V

    :cond_1
    return-void
.end method

.method private showPickerContent()V
    .locals 2

    .line 370
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->pickerEmptySearchResultsView:Landroid/view/View;

    invoke-virtual {v0}, Landroid/view/View;->isShown()Z

    move-result v0

    if-eqz v0, :cond_0

    .line 371
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->pickerEmptySearchResultsView:Landroid/view/View;

    const/16 v1, 0x8

    invoke-virtual {v0, v1}, Landroid/view/View;->setVisibility(I)V

    .line 374
    :cond_0
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->pickerOptionsRecycler:Landroidx/recyclerview/widget/RecyclerView;

    invoke-virtual {v0}, Landroidx/recyclerview/widget/RecyclerView;->isShown()Z

    move-result v0

    if-nez v0, :cond_1

    .line 375
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->pickerOptionsRecycler:Landroidx/recyclerview/widget/RecyclerView;

    const/4 v1, 0x0

    invoke-virtual {v0, v1}, Landroidx/recyclerview/widget/RecyclerView;->setVisibility(I)V

    :cond_1
    return-void
.end method

.method private showScrollJumperView(Z)V
    .locals 2

    .line 1242
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->scrollIndicator:Landroid/view/View;

    const/4 v1, 0x0

    invoke-virtual {v0, v1}, Landroid/view/View;->setVisibility(I)V

    if-eqz p1, :cond_0

    .line 1244
    iget-object p1, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->unreadMessagesIndicatorDot:Landroid/view/View;

    invoke-virtual {p1, v1}, Landroid/view/View;->setVisibility(I)V

    .line 1245
    iget-object p1, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->context:Landroid/content/Context;

    sget v0, Lcom/helpshift/R$string;->hs__jump_button_with_new_message_voice_over:I

    invoke-virtual {p1, v0}, Landroid/content/Context;->getString(I)Ljava/lang/String;

    move-result-object p1

    goto :goto_0

    .line 1248
    :cond_0
    iget-object p1, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->unreadMessagesIndicatorDot:Landroid/view/View;

    const/16 v0, 0x8

    invoke-virtual {p1, v0}, Landroid/view/View;->setVisibility(I)V

    .line 1249
    iget-object p1, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->context:Landroid/content/Context;

    sget v0, Lcom/helpshift/R$string;->hs__jump_button_voice_over:I

    invoke-virtual {p1, v0}, Landroid/content/Context;->getString(I)Ljava/lang/String;

    move-result-object p1

    .line 1251
    :goto_0
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->scrollJumpButton:Landroid/view/View;

    invoke-virtual {v0, p1}, Landroid/view/View;->setContentDescription(Ljava/lang/CharSequence;)V

    return-void
.end method

.method private showSendReplyUI(Lcom/helpshift/conversation/activeconversation/message/input/Input;)V
    .locals 1

    if-nez p1, :cond_0

    .line 729
    invoke-virtual {p0}, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->setMessagesViewBottomPadding()V

    .line 730
    iget-object p1, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->replyBoxView:Landroid/view/View;

    const/4 v0, 0x0

    invoke-virtual {p1, v0}, Landroid/view/View;->setVisibility(I)V

    .line 733
    iget-object p1, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->parentView:Landroid/view/View;

    sget v0, Lcom/helpshift/R$id;->replyBoxLabelLayout:I

    invoke-virtual {p1, v0}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p1

    check-cast p1, Landroid/widget/LinearLayout;

    const/16 v0, 0x8

    .line 734
    invoke-virtual {p1, v0}, Landroid/widget/LinearLayout;->setVisibility(I)V

    .line 737
    iget-object p1, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->replyField:Landroid/widget/EditText;

    const/4 v0, 0x1

    invoke-virtual {p1, v0}, Landroid/widget/EditText;->setFocusableInTouchMode(Z)V

    .line 738
    iget-object p1, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->replyField:Landroid/widget/EditText;

    const/4 v0, 0x0

    invoke-virtual {p1, v0}, Landroid/widget/EditText;->setOnClickListener(Landroid/view/View$OnClickListener;)V

    .line 739
    invoke-direct {p0}, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->resetReplyFieldToNormalTextInput()V

    .line 740
    invoke-virtual {p0}, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->hideSkipButton()V

    return-void

    .line 745
    :cond_0
    instance-of v0, p1, Lcom/helpshift/conversation/activeconversation/message/input/TextInput;

    if-eqz v0, :cond_1

    .line 746
    check-cast p1, Lcom/helpshift/conversation/activeconversation/message/input/TextInput;

    invoke-direct {p0, p1}, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->renderForTextInput(Lcom/helpshift/conversation/activeconversation/message/input/TextInput;)V

    .line 751
    :cond_1
    invoke-virtual {p0}, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->setMessagesViewBottomPadding()V

    return-void
.end method


# virtual methods
.method public appendMessages(II)V
    .locals 1

    .line 925
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->messagesAdapter:Lcom/helpshift/support/conversations/MessagesAdapter;

    if-nez v0, :cond_0

    return-void

    .line 929
    :cond_0
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->messagesAdapter:Lcom/helpshift/support/conversations/MessagesAdapter;

    invoke-virtual {v0, p1, p2}, Lcom/helpshift/support/conversations/MessagesAdapter;->onItemRangeInserted(II)V

    return-void
.end method

.method createDatePickerForReplyField()Landroid/app/DatePickerDialog;
    .locals 7

    .line 807
    new-instance v2, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer$14;

    invoke-direct {v2, p0}, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer$14;-><init>(Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;)V

    .line 820
    invoke-static {}, Ljava/util/Calendar;->getInstance()Ljava/util/Calendar;

    move-result-object v0

    .line 822
    :try_start_0
    iget-object v1, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->replyField:Landroid/widget/EditText;

    invoke-virtual {v1}, Landroid/widget/EditText;->getText()Landroid/text/Editable;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v1

    .line 823
    invoke-static {v1}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v3

    if-nez v3, :cond_0

    .line 824
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getCoreApi()Lcom/helpshift/CoreApi;

    move-result-object v3

    invoke-interface {v3}, Lcom/helpshift/CoreApi;->getLocaleProviderDM()Lcom/helpshift/localeprovider/domainmodel/LocaleProviderDM;

    move-result-object v3

    invoke-virtual {v3}, Lcom/helpshift/localeprovider/domainmodel/LocaleProviderDM;->getCurrentLocale()Ljava/util/Locale;

    move-result-object v3

    const-string v4, "EEEE, MMMM dd, yyyy"

    .line 826
    invoke-static {v4, v3}, Lcom/helpshift/common/util/HSDateFormatSpec;->getDateFormatter(Ljava/lang/String;Ljava/util/Locale;)Lcom/helpshift/common/util/HSSimpleDateFormat;

    move-result-object v3

    invoke-virtual {v3, v1}, Lcom/helpshift/common/util/HSSimpleDateFormat;->parse(Ljava/lang/String;)Ljava/util/Date;

    move-result-object v1

    .line 827
    invoke-virtual {v0, v1}, Ljava/util/Calendar;->setTime(Ljava/util/Date;)V
    :try_end_0
    .catch Ljava/text/ParseException; {:try_start_0 .. :try_end_0} :catch_0

    .line 835
    :catch_0
    :cond_0
    new-instance v6, Landroid/app/DatePickerDialog;

    iget-object v1, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->parentView:Landroid/view/View;

    invoke-virtual {v1}, Landroid/view/View;->getContext()Landroid/content/Context;

    move-result-object v1

    const/4 v3, 0x1

    .line 837
    invoke-virtual {v0, v3}, Ljava/util/Calendar;->get(I)I

    move-result v3

    const/4 v4, 0x2

    .line 838
    invoke-virtual {v0, v4}, Ljava/util/Calendar;->get(I)I

    move-result v4

    const/4 v5, 0x5

    .line 839
    invoke-virtual {v0, v5}, Ljava/util/Calendar;->get(I)I

    move-result v5

    move-object v0, v6

    invoke-direct/range {v0 .. v5}, Landroid/app/DatePickerDialog;-><init>(Landroid/content/Context;Landroid/app/DatePickerDialog$OnDateSetListener;III)V

    return-object v6
.end method

.method public destroy()V
    .locals 1

    const/4 v0, 0x1

    .line 717
    invoke-virtual {p0, v0}, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->hideListPicker(Z)V

    const/4 v0, 0x0

    .line 719
    iput-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->conversationalFragmentRouter:Lcom/helpshift/support/conversations/ConversationalFragmentRouter;

    return-void
.end method

.method public disableSendReplyButton()V
    .locals 3

    .line 986
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->replyButton:Landroid/widget/ImageButton;

    const/4 v1, 0x0

    invoke-virtual {v0, v1}, Landroid/widget/ImageButton;->setEnabled(Z)V

    .line 987
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->context:Landroid/content/Context;

    sget v2, Lcom/helpshift/R$attr;->hs__reply_button_disabled_alpha:I

    invoke-static {v0, v2}, Lcom/helpshift/support/util/Styles;->getInt(Landroid/content/Context;I)I

    move-result v0

    .line 988
    iget-object v2, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->replyButton:Landroid/widget/ImageButton;

    invoke-static {v2, v0}, Lcom/helpshift/support/util/Styles;->setImageAlpha(Landroid/widget/ImageButton;I)V

    .line 989
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->context:Landroid/content/Context;

    iget-object v2, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->replyButton:Landroid/widget/ImageButton;

    invoke-virtual {v2}, Landroid/widget/ImageButton;->getDrawable()Landroid/graphics/drawable/Drawable;

    move-result-object v2

    invoke-static {v0, v2, v1}, Lcom/helpshift/support/util/Styles;->setSendMessageButtonIconColor(Landroid/content/Context;Landroid/graphics/drawable/Drawable;Z)V

    return-void
.end method

.method public enableSendReplyButton()V
    .locals 3

    .line 979
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->replyButton:Landroid/widget/ImageButton;

    const/4 v1, 0x1

    invoke-virtual {v0, v1}, Landroid/widget/ImageButton;->setEnabled(Z)V

    .line 980
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->replyButton:Landroid/widget/ImageButton;

    const/16 v2, 0xff

    invoke-static {v0, v2}, Lcom/helpshift/support/util/Styles;->setImageAlpha(Landroid/widget/ImageButton;I)V

    .line 981
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->context:Landroid/content/Context;

    iget-object v2, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->replyButton:Landroid/widget/ImageButton;

    invoke-virtual {v2}, Landroid/widget/ImageButton;->getDrawable()Landroid/graphics/drawable/Drawable;

    move-result-object v2

    invoke-static {v0, v2, v1}, Lcom/helpshift/support/util/Styles;->setSendMessageButtonIconColor(Landroid/content/Context;Landroid/graphics/drawable/Drawable;Z)V

    return-void
.end method

.method public getReply()Ljava/lang/String;
    .locals 1

    .line 994
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->replyField:Landroid/widget/EditText;

    invoke-virtual {v0}, Landroid/widget/EditText;->getText()Landroid/text/Editable;

    move-result-object v0

    invoke-virtual {v0}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method public hideAgentTypingIndicator()V
    .locals 2

    .line 1162
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->messagesAdapter:Lcom/helpshift/support/conversations/MessagesAdapter;

    if-eqz v0, :cond_0

    .line 1163
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->messagesAdapter:Lcom/helpshift/support/conversations/MessagesAdapter;

    const/4 v1, 0x0

    invoke-virtual {v0, v1}, Lcom/helpshift/support/conversations/MessagesAdapter;->setAgentTypingIndicatorVisibility(Z)V

    :cond_0
    return-void
.end method

.method public hideKeyboard()V
    .locals 2

    .line 1222
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->context:Landroid/content/Context;

    iget-object v1, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->replyField:Landroid/widget/EditText;

    invoke-static {v0, v1}, Lcom/helpshift/support/util/KeyboardUtil;->hideKeyboard(Landroid/content/Context;Landroid/view/View;)V

    return-void
.end method

.method public hideListPicker(Z)V
    .locals 1

    .line 314
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->bottomSheetBehavior:Lcom/google/android/material/bottomsheet/BottomSheetBehavior;

    if-eqz v0, :cond_2

    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->pickerBottomSheet:Lcom/helpshift/views/bottomsheet/HSBottomSheet;

    if-nez v0, :cond_0

    goto :goto_1

    :cond_0
    if-eqz p1, :cond_1

    .line 319
    iget-object p1, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->bottomSheetBehavior:Lcom/google/android/material/bottomsheet/BottomSheetBehavior;

    const/4 v0, 0x1

    invoke-virtual {p1, v0}, Lcom/google/android/material/bottomsheet/BottomSheetBehavior;->setHideable(Z)V

    .line 320
    iget-object p1, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->pickerBottomSheet:Lcom/helpshift/views/bottomsheet/HSBottomSheet;

    invoke-virtual {p1}, Lcom/helpshift/views/bottomsheet/HSBottomSheet;->removeAllBottomSheetCallbacks()V

    .line 321
    iget-object p1, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->pickerBottomSheet:Lcom/helpshift/views/bottomsheet/HSBottomSheet;

    new-instance v0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer$2;

    invoke-direct {v0, p0}, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer$2;-><init>(Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;)V

    invoke-virtual {p1, v0}, Lcom/helpshift/views/bottomsheet/HSBottomSheet;->addBottomSheetCallback(Lcom/google/android/material/bottomsheet/BottomSheetBehavior$BottomSheetCallback;)V

    .line 335
    iget-object p1, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->bottomSheetBehavior:Lcom/google/android/material/bottomsheet/BottomSheetBehavior;

    const/4 v0, 0x5

    invoke-virtual {p1, v0}, Lcom/google/android/material/bottomsheet/BottomSheetBehavior;->setState(I)V

    goto :goto_0

    .line 338
    :cond_1
    invoke-virtual {p0}, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->removePickerViewFromWindow()V

    .line 341
    :goto_0
    invoke-direct {p0}, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->resetAccessibilityForToolbar()V

    .line 343
    invoke-virtual {p0}, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->hideKeyboard()V

    .line 344
    iget-object p1, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->parentView:Landroid/view/View;

    const/4 v0, 0x0

    invoke-direct {p0, p1, v0}, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->setBottomOffset(Landroid/view/View;I)V

    .line 345
    invoke-virtual {p0}, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->hideSkipButton()V

    return-void

    :cond_2
    :goto_1
    return-void
.end method

.method public hideNetworkErrorFooter()V
    .locals 2

    .line 300
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->networkErrorFooter:Landroid/widget/LinearLayout;

    const/16 v1, 0x8

    invoke-virtual {v0, v1}, Landroid/widget/LinearLayout;->setVisibility(I)V

    return-void
.end method

.method public hidePickerClearButton()V
    .locals 2

    .line 498
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->pickerClearView:Landroid/widget/ImageView;

    invoke-virtual {v0}, Landroid/widget/ImageView;->isShown()Z

    move-result v0

    if-eqz v0, :cond_0

    .line 499
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->pickerClearView:Landroid/widget/ImageView;

    const/16 v1, 0x8

    invoke-virtual {v0, v1}, Landroid/widget/ImageView;->setVisibility(I)V

    :cond_0
    return-void
.end method

.method public hideReplyValidationFailedError()V
    .locals 2

    .line 269
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->replyValidationFailedView:Landroid/widget/TextView;

    const/16 v1, 0x8

    invoke-virtual {v0, v1}, Landroid/widget/TextView;->setVisibility(I)V

    return-void
.end method

.method public hideSendReplyUI()V
    .locals 2

    .line 756
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->messagesRecyclerView:Landroidx/recyclerview/widget/RecyclerView;

    const/4 v1, 0x0

    invoke-virtual {v0, v1, v1, v1, v1}, Landroidx/recyclerview/widget/RecyclerView;->setPadding(IIII)V

    .line 757
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->replyBoxView:Landroid/view/View;

    const/16 v1, 0x8

    invoke-virtual {v0, v1}, Landroid/view/View;->setVisibility(I)V

    .line 758
    invoke-virtual {p0}, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->hideSkipButton()V

    return-void
.end method

.method public hideSkipButton()V
    .locals 2

    .line 201
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->skipOutterBubble:Landroid/widget/LinearLayout;

    const/16 v1, 0x8

    invoke-virtual {v0, v1}, Landroid/widget/LinearLayout;->setVisibility(I)V

    .line 202
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->messagesRecyclerView:Landroidx/recyclerview/widget/RecyclerView;

    iget-object v1, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->lastMessageItemDecor:Landroidx/recyclerview/widget/RecyclerView$ItemDecoration;

    invoke-virtual {v0, v1}, Landroidx/recyclerview/widget/RecyclerView;->removeItemDecoration(Landroidx/recyclerview/widget/RecyclerView$ItemDecoration;)V

    return-void
.end method

.method public initializeMessages(Ljava/util/List;)V
    .locals 3
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/message/MessageDM;",
            ">;)V"
        }
    .end annotation

    .line 913
    new-instance v0, Lcom/helpshift/support/conversations/MessagesAdapter;

    iget-object v1, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->context:Landroid/content/Context;

    iget-object v2, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->conversationalFragmentRouter:Lcom/helpshift/support/conversations/ConversationalFragmentRouter;

    invoke-direct {v0, v1, p1, v2}, Lcom/helpshift/support/conversations/MessagesAdapter;-><init>(Landroid/content/Context;Ljava/util/List;Lcom/helpshift/support/conversations/messages/MessagesAdapterClickListener;)V

    iput-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->messagesAdapter:Lcom/helpshift/support/conversations/MessagesAdapter;

    .line 916
    new-instance p1, Landroidx/recyclerview/widget/LinearLayoutManager;

    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->context:Landroid/content/Context;

    invoke-direct {p1, v0}, Landroidx/recyclerview/widget/LinearLayoutManager;-><init>(Landroid/content/Context;)V

    const/4 v0, 0x1

    .line 917
    invoke-virtual {p1, v0}, Landroidx/recyclerview/widget/LinearLayoutManager;->setStackFromEnd(Z)V

    .line 918
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->messagesRecyclerView:Landroidx/recyclerview/widget/RecyclerView;

    invoke-virtual {v0, p1}, Landroidx/recyclerview/widget/RecyclerView;->setLayoutManager(Landroidx/recyclerview/widget/RecyclerView$LayoutManager;)V

    .line 919
    iget-object p1, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->messagesRecyclerView:Landroidx/recyclerview/widget/RecyclerView;

    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->messagesAdapter:Lcom/helpshift/support/conversations/MessagesAdapter;

    invoke-virtual {p1, v0}, Landroidx/recyclerview/widget/RecyclerView;->setAdapter(Landroidx/recyclerview/widget/RecyclerView$Adapter;)V

    return-void
.end method

.method public isReplyBoxVisible()Z
    .locals 1

    .line 1176
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->replyBoxView:Landroid/view/View;

    invoke-virtual {v0}, Landroid/view/View;->getVisibility()I

    move-result v0

    if-nez v0, :cond_0

    const/4 v0, 0x1

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    return v0
.end method

.method public launchAttachment(Ljava/lang/String;Ljava/lang/String;)V
    .locals 2

    .line 1080
    invoke-static {p1}, Lcom/helpshift/android/commons/downloader/HsUriUtils;->isValidUriPath(Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_0

    .line 1081
    invoke-static {p1}, Landroid/net/Uri;->parse(Ljava/lang/String;)Landroid/net/Uri;

    move-result-object p1

    .line 1082
    new-instance v0, Landroid/content/Intent;

    const-string v1, "android.intent.action.VIEW"

    invoke-direct {v0, v1}, Landroid/content/Intent;-><init>(Ljava/lang/String;)V

    const/4 v1, 0x1

    .line 1083
    invoke-virtual {v0, v1}, Landroid/content/Intent;->setFlags(I)Landroid/content/Intent;

    .line 1084
    invoke-virtual {v0, p1, p2}, Landroid/content/Intent;->setDataAndType(Landroid/net/Uri;Ljava/lang/String;)Landroid/content/Intent;

    .line 1085
    invoke-direct {p0, v0, p1}, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->launchAttachmentIntentInternal(Landroid/content/Intent;Landroid/net/Uri;)V

    goto :goto_1

    .line 1088
    :cond_0
    invoke-static {p1}, Lcom/helpshift/common/util/FileUtil;->validateAndCreateFile(Ljava/lang/String;)Ljava/io/File;

    move-result-object p1

    if-eqz p1, :cond_2

    .line 1092
    sget v0, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 v1, 0x18

    if-lt v0, v1, :cond_1

    .line 1093
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->context:Landroid/content/Context;

    invoke-static {v0, p1, p2}, Lcom/helpshift/util/IntentUtil;->createFileProviderIntent(Landroid/content/Context;Ljava/io/File;Ljava/lang/String;)Landroid/content/Intent;

    move-result-object p2

    goto :goto_0

    .line 1096
    :cond_1
    new-instance v0, Landroid/content/Intent;

    const-string v1, "android.intent.action.VIEW"

    invoke-direct {v0, v1}, Landroid/content/Intent;-><init>(Ljava/lang/String;)V

    .line 1097
    invoke-static {p1}, Landroid/net/Uri;->fromFile(Ljava/io/File;)Landroid/net/Uri;

    move-result-object v1

    .line 1098
    invoke-virtual {v0, v1, p2}, Landroid/content/Intent;->setDataAndType(Landroid/net/Uri;Ljava/lang/String;)Landroid/content/Intent;

    move-object p2, v0

    .line 1100
    :goto_0
    invoke-direct {p0, p2, p1}, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->launchAttachmentIntentInternal(Landroid/content/Intent;Ljava/io/File;)V

    goto :goto_1

    .line 1103
    :cond_2
    sget-object p1, Lcom/helpshift/common/exception/PlatformException;->FILE_NOT_FOUND:Lcom/helpshift/common/exception/PlatformException;

    invoke-virtual {p0, p1}, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->showErrorView(Lcom/helpshift/common/exception/ExceptionType;)V

    :goto_1
    return-void
.end method

.method public launchScreenshotAttachment(Ljava/lang/String;Ljava/lang/String;)V
    .locals 1

    .line 1069
    invoke-static {p1}, Lcom/helpshift/common/util/FileUtil;->validateAndCreateFile(Ljava/lang/String;)Ljava/io/File;

    move-result-object p1

    if-eqz p1, :cond_0

    .line 1071
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->context:Landroid/content/Context;

    invoke-static {v0, p1, p2}, Lcom/helpshift/util/IntentUtil;->createFileProviderIntent(Landroid/content/Context;Ljava/io/File;Ljava/lang/String;)Landroid/content/Intent;

    move-result-object p2

    invoke-direct {p0, p2, p1}, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->launchAttachmentIntentInternal(Landroid/content/Intent;Ljava/io/File;)V

    goto :goto_0

    .line 1074
    :cond_0
    sget-object p1, Lcom/helpshift/common/exception/PlatformException;->FILE_NOT_FOUND:Lcom/helpshift/common/exception/PlatformException;

    invoke-virtual {p0, p1}, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->showErrorView(Lcom/helpshift/common/exception/ExceptionType;)V

    :goto_0
    return-void
.end method

.method public notifyRefreshList()V
    .locals 1

    .line 1226
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->messagesAdapter:Lcom/helpshift/support/conversations/MessagesAdapter;

    if-eqz v0, :cond_0

    .line 1227
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->messagesAdapter:Lcom/helpshift/support/conversations/MessagesAdapter;

    invoke-virtual {v0}, Lcom/helpshift/support/conversations/MessagesAdapter;->notifyDataSetChanged()V

    :cond_0
    return-void
.end method

.method public onAuthenticationFailure()V
    .locals 1

    .line 1169
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->conversationalFragmentRouter:Lcom/helpshift/support/conversations/ConversationalFragmentRouter;

    if-eqz v0, :cond_0

    .line 1170
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->conversationalFragmentRouter:Lcom/helpshift/support/conversations/ConversationalFragmentRouter;

    invoke-interface {v0}, Lcom/helpshift/support/conversations/ConversationalFragmentRouter;->onAuthenticationFailure()V

    :cond_0
    return-void
.end method

.method public onBackPressed()Z
    .locals 2

    .line 513
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->pickerBottomSheet:Lcom/helpshift/views/bottomsheet/HSBottomSheet;

    if-eqz v0, :cond_0

    .line 514
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->bottomSheetBehavior:Lcom/google/android/material/bottomsheet/BottomSheetBehavior;

    invoke-virtual {v0}, Lcom/google/android/material/bottomsheet/BottomSheetBehavior;->getState()I

    move-result v0

    const/4 v1, 0x3

    if-ne v0, v1, :cond_0

    .line 515
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->bottomSheetBehavior:Lcom/google/android/material/bottomsheet/BottomSheetBehavior;

    const/4 v1, 0x4

    invoke-virtual {v0, v1}, Lcom/google/android/material/bottomsheet/BottomSheetBehavior;->setState(I)V

    const/4 v0, 0x1

    return v0

    :cond_0
    const/4 v0, 0x0

    return v0
.end method

.method public onFocusChanged(Z)V
    .locals 0

    if-nez p1, :cond_0

    const/4 p1, 0x1

    .line 710
    invoke-virtual {p0, p1}, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->hideListPicker(Z)V

    :cond_0
    return-void
.end method

.method public openAppReviewStore(Ljava/lang/String;)V
    .locals 2

    .line 1115
    new-instance v0, Landroid/content/Intent;

    const-string v1, "android.intent.action.VIEW"

    invoke-direct {v0, v1}, Landroid/content/Intent;-><init>(Ljava/lang/String;)V

    .line 1116
    invoke-static {p1}, Landroid/net/Uri;->parse(Ljava/lang/String;)Landroid/net/Uri;

    move-result-object p1

    invoke-virtual {v0, p1}, Landroid/content/Intent;->setData(Landroid/net/Uri;)Landroid/content/Intent;

    .line 1117
    iget-object p1, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->context:Landroid/content/Context;

    invoke-virtual {p1}, Landroid/content/Context;->getPackageManager()Landroid/content/pm/PackageManager;

    move-result-object p1

    invoke-virtual {v0, p1}, Landroid/content/Intent;->resolveActivity(Landroid/content/pm/PackageManager;)Landroid/content/ComponentName;

    move-result-object p1

    if-eqz p1, :cond_0

    .line 1118
    iget-object p1, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->context:Landroid/content/Context;

    invoke-virtual {p1, v0}, Landroid/content/Context;->startActivity(Landroid/content/Intent;)V

    goto :goto_0

    .line 1121
    :cond_0
    sget-object p1, Lcom/helpshift/common/exception/PlatformException;->NO_APPS_FOR_OPENING_ATTACHMENT:Lcom/helpshift/common/exception/PlatformException;

    invoke-virtual {p0, p1}, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->showErrorView(Lcom/helpshift/common/exception/ExceptionType;)V

    :goto_0
    return-void
.end method

.method public openFreshConversationScreen(Ljava/util/Map;)V
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/Boolean;",
            ">;)V"
        }
    .end annotation

    .line 1150
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->conversationalFragmentRouter:Lcom/helpshift/support/conversations/ConversationalFragmentRouter;

    invoke-interface {v0, p1}, Lcom/helpshift/support/conversations/ConversationalFragmentRouter;->openFreshConversationScreen(Ljava/util/Map;)V

    return-void
.end method

.method public removeMessages(II)V
    .locals 1

    .line 950
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->messagesAdapter:Lcom/helpshift/support/conversations/MessagesAdapter;

    if-nez v0, :cond_0

    return-void

    .line 954
    :cond_0
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->messagesAdapter:Lcom/helpshift/support/conversations/MessagesAdapter;

    invoke-virtual {v0, p1, p2}, Lcom/helpshift/support/conversations/MessagesAdapter;->onItemRangeRemoved(II)V

    return-void
.end method

.method removePickerViewFromWindow()V
    .locals 1

    .line 349
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->pickerBottomSheet:Lcom/helpshift/views/bottomsheet/HSBottomSheet;

    invoke-virtual {v0}, Lcom/helpshift/views/bottomsheet/HSBottomSheet;->remove()V

    const/4 v0, 0x0

    .line 350
    iput-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->pickerBottomSheet:Lcom/helpshift/views/bottomsheet/HSBottomSheet;

    return-void
.end method

.method public requestReplyFieldFocus()V
    .locals 1

    .line 1212
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->replyField:Landroid/widget/EditText;

    invoke-virtual {v0}, Landroid/widget/EditText;->requestFocus()Z

    return-void
.end method

.method resetPickerSearchViewToNormalHeader()V
    .locals 4

    .line 646
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->pickerHeaderSearchView:Landroid/widget/EditText;

    const/16 v1, 0x8

    invoke-virtual {v0, v1}, Landroid/widget/EditText;->setVisibility(I)V

    .line 647
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->pickerExpandedHeaderText:Landroid/widget/TextView;

    const/4 v2, 0x0

    invoke-virtual {v0, v2}, Landroid/widget/TextView;->setVisibility(I)V

    .line 648
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->pickerHeaderSearchView:Landroid/widget/EditText;

    const-string v3, ""

    invoke-virtual {v0, v3}, Landroid/widget/EditText;->setText(Ljava/lang/CharSequence;)V

    .line 649
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->pickerBackView:Landroid/widget/ImageView;

    invoke-virtual {v0, v1}, Landroid/widget/ImageView;->setVisibility(I)V

    .line 650
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->pickerCollapseView:Landroid/widget/ImageView;

    invoke-virtual {v0, v2}, Landroid/widget/ImageView;->setVisibility(I)V

    .line 651
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->pickerClearView:Landroid/widget/ImageView;

    invoke-virtual {v0, v1}, Landroid/widget/ImageView;->setVisibility(I)V

    .line 652
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->pickerSearchView:Landroid/widget/ImageView;

    invoke-virtual {v0, v2}, Landroid/widget/ImageView;->setVisibility(I)V

    .line 653
    invoke-virtual {p0}, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->hideKeyboard()V

    .line 654
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->pickerBottomSheet:Lcom/helpshift/views/bottomsheet/HSBottomSheet;

    const/4 v1, 0x1

    invoke-virtual {v0, v1}, Lcom/helpshift/views/bottomsheet/HSBottomSheet;->setDraggable(Z)V

    return-void
.end method

.method public scrollToBottom()V
    .locals 2

    .line 1262
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->messagesAdapter:Lcom/helpshift/support/conversations/MessagesAdapter;

    if-nez v0, :cond_0

    return-void

    .line 1266
    :cond_0
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->messagesAdapter:Lcom/helpshift/support/conversations/MessagesAdapter;

    invoke-virtual {v0}, Lcom/helpshift/support/conversations/MessagesAdapter;->getItemCount()I

    move-result v0

    if-lez v0, :cond_1

    .line 1268
    iget-object v1, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->messagesRecyclerView:Landroidx/recyclerview/widget/RecyclerView;

    add-int/lit8 v0, v0, -0x1

    invoke-virtual {v1, v0}, Landroidx/recyclerview/widget/RecyclerView;->scrollToPosition(I)V

    :cond_1
    return-void
.end method

.method protected setMessagesViewBottomPadding()V
    .locals 3

    .line 1273
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->context:Landroid/content/Context;

    const/high16 v1, 0x41400000    # 12.0f

    invoke-static {v0, v1}, Lcom/helpshift/util/Styles;->dpToPx(Landroid/content/Context;F)F

    move-result v0

    float-to-int v0, v0

    .line 1274
    iget-object v1, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->messagesRecyclerView:Landroidx/recyclerview/widget/RecyclerView;

    const/4 v2, 0x0

    invoke-virtual {v1, v2, v2, v2, v0}, Landroidx/recyclerview/widget/RecyclerView;->setPadding(IIII)V

    return-void
.end method

.method public setReply(Ljava/lang/String;)V
    .locals 1

    .line 999
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->replyField:Landroid/widget/EditText;

    invoke-virtual {v0, p1}, Landroid/widget/EditText;->setText(Ljava/lang/CharSequence;)V

    .line 1000
    iget-object p1, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->replyField:Landroid/widget/EditText;

    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->replyField:Landroid/widget/EditText;

    invoke-virtual {v0}, Landroid/widget/EditText;->getText()Landroid/text/Editable;

    move-result-object v0

    invoke-interface {v0}, Landroid/text/Editable;->length()I

    move-result v0

    invoke-virtual {p1, v0}, Landroid/widget/EditText;->setSelection(I)V

    return-void
.end method

.method public setReplyboxListeners()V
    .locals 2

    .line 1181
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->replyField:Landroid/widget/EditText;

    new-instance v1, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer$16;

    invoke-direct {v1, p0}, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer$16;-><init>(Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;)V

    invoke-virtual {v0, v1}, Landroid/widget/EditText;->addTextChangedListener(Landroid/text/TextWatcher;)V

    .line 1190
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->replyField:Landroid/widget/EditText;

    new-instance v1, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer$17;

    invoke-direct {v1, p0}, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer$17;-><init>(Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;)V

    invoke-virtual {v0, v1}, Landroid/widget/EditText;->setOnEditorActionListener(Landroid/widget/TextView$OnEditorActionListener;)V

    .line 1199
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->replyButton:Landroid/widget/ImageButton;

    new-instance v1, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer$18;

    invoke-direct {v1, p0}, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer$18;-><init>(Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;)V

    invoke-virtual {v0, v1}, Landroid/widget/ImageButton;->setOnClickListener(Landroid/view/View$OnClickListener;)V

    return-void
.end method

.method public showAgentTypingIndicator()V
    .locals 2

    .line 1155
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->messagesAdapter:Lcom/helpshift/support/conversations/MessagesAdapter;

    if-eqz v0, :cond_0

    .line 1156
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->messagesAdapter:Lcom/helpshift/support/conversations/MessagesAdapter;

    const/4 v1, 0x1

    invoke-virtual {v0, v1}, Lcom/helpshift/support/conversations/MessagesAdapter;->setAgentTypingIndicatorVisibility(Z)V

    :cond_0
    return-void
.end method

.method public showCSATSubmittedView()V
    .locals 3

    .line 1143
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->parentView:Landroid/view/View;

    iget-object v1, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->context:Landroid/content/Context;

    invoke-virtual {v1}, Landroid/content/Context;->getResources()Landroid/content/res/Resources;

    move-result-object v1

    sget v2, Lcom/helpshift/R$string;->hs__csat_submit_toast:I

    .line 1144
    invoke-virtual {v1, v2}, Landroid/content/res/Resources;->getString(I)Ljava/lang/String;

    move-result-object v1

    const/4 v2, 0x0

    .line 1143
    invoke-static {v0, v1, v2}, Lcom/helpshift/support/util/SnackbarUtil;->showSnackbar(Landroid/view/View;Ljava/lang/CharSequence;I)V

    return-void
.end method

.method public showEmptyListPickerView()V
    .locals 0

    .line 366
    invoke-direct {p0}, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->showEmptyPickerView()V

    return-void
.end method

.method public showErrorView(Lcom/helpshift/common/exception/ExceptionType;)V
    .locals 1

    .line 1110
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->parentView:Landroid/view/View;

    invoke-static {p1, v0}, Lcom/helpshift/support/util/SnackbarUtil;->showSnackbar(Lcom/helpshift/common/exception/ExceptionType;Landroid/view/View;)V

    return-void
.end method

.method public showKeyboard()V
    .locals 2

    .line 1217
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->context:Landroid/content/Context;

    iget-object v1, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->replyField:Landroid/widget/EditText;

    invoke-static {v0, v1}, Lcom/helpshift/support/util/KeyboardUtil;->showKeyboard(Landroid/content/Context;Landroid/view/View;)V

    return-void
.end method

.method public showListPicker(Ljava/util/List;Ljava/lang/String;ZLjava/lang/String;)V
    .locals 4
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/viewmodel/OptionUIModel;",
            ">;",
            "Ljava/lang/String;",
            "Z",
            "Ljava/lang/String;",
            ")V"
        }
    .end annotation

    .line 392
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->pickerBottomSheet:Lcom/helpshift/views/bottomsheet/HSBottomSheet;

    if-eqz v0, :cond_0

    return-void

    .line 397
    :cond_0
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->parentView:Landroid/view/View;

    invoke-virtual {v0}, Landroid/view/View;->getContext()Landroid/content/Context;

    move-result-object v0

    invoke-static {v0}, Lcom/helpshift/support/util/Styles;->isTablet(Landroid/content/Context;)Z

    move-result v0

    if-eqz v0, :cond_1

    const v1, 0x3f4ccccd    # 0.8f

    goto :goto_0

    :cond_1
    const/high16 v1, 0x3f800000    # 1.0f

    .line 401
    :goto_0
    new-instance v2, Lcom/helpshift/views/bottomsheet/HSBottomSheet$Builder;

    iget-object v3, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->listPickerHostWindow:Landroid/view/Window;

    invoke-direct {v2, v3}, Lcom/helpshift/views/bottomsheet/HSBottomSheet$Builder;-><init>(Landroid/view/Window;)V

    sget v3, Lcom/helpshift/R$layout;->hs__picker_layout:I

    .line 402
    invoke-virtual {v2, v3}, Lcom/helpshift/views/bottomsheet/HSBottomSheet$Builder;->contentView(I)Lcom/helpshift/views/bottomsheet/HSBottomSheet$Builder;

    move-result-object v2

    iget-object v3, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->messagesRecyclerView:Landroidx/recyclerview/widget/RecyclerView;

    .line 403
    invoke-virtual {v2, v3}, Lcom/helpshift/views/bottomsheet/HSBottomSheet$Builder;->referenceView(Landroid/view/View;)Lcom/helpshift/views/bottomsheet/HSBottomSheet$Builder;

    move-result-object v2

    const/4 v3, 0x1

    .line 404
    invoke-virtual {v2, v3}, Lcom/helpshift/views/bottomsheet/HSBottomSheet$Builder;->enableDimAnimation(Z)Lcom/helpshift/views/bottomsheet/HSBottomSheet$Builder;

    move-result-object v2

    .line 405
    invoke-virtual {v2, v1}, Lcom/helpshift/views/bottomsheet/HSBottomSheet$Builder;->dimOpacity(F)Lcom/helpshift/views/bottomsheet/HSBottomSheet$Builder;

    move-result-object v1

    .line 406
    invoke-virtual {v1}, Lcom/helpshift/views/bottomsheet/HSBottomSheet$Builder;->inflateAndBuild()Lcom/helpshift/views/bottomsheet/HSBottomSheet;

    move-result-object v1

    iput-object v1, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->pickerBottomSheet:Lcom/helpshift/views/bottomsheet/HSBottomSheet;

    .line 407
    invoke-direct {p0, p2}, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->initPickerViews(Ljava/lang/String;)V

    .line 408
    iget-object p2, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->bottomSheetBehavior:Lcom/google/android/material/bottomsheet/BottomSheetBehavior;

    iget-object v1, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->context:Landroid/content/Context;

    const/high16 v2, 0x430e0000    # 142.0f

    invoke-static {v1, v2}, Lcom/helpshift/util/Styles;->dpToPx(Landroid/content/Context;F)F

    move-result v1

    float-to-int v1, v1

    invoke-virtual {p2, v1}, Lcom/google/android/material/bottomsheet/BottomSheetBehavior;->setPeekHeight(I)V

    .line 409
    new-instance p2, Lcom/helpshift/support/conversations/picker/PickerAdapter;

    iget-object v1, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->conversationalFragmentRouter:Lcom/helpshift/support/conversations/ConversationalFragmentRouter;

    invoke-direct {p2, p1, v1}, Lcom/helpshift/support/conversations/picker/PickerAdapter;-><init>(Ljava/util/List;Lcom/helpshift/support/conversations/ConversationalFragmentRouter;)V

    iput-object p2, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->pickerAdapter:Lcom/helpshift/support/conversations/picker/PickerAdapter;

    .line 410
    iget-object p1, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->pickerOptionsRecycler:Landroidx/recyclerview/widget/RecyclerView;

    iget-object p2, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->pickerAdapter:Lcom/helpshift/support/conversations/picker/PickerAdapter;

    invoke-virtual {p1, p2}, Landroidx/recyclerview/widget/RecyclerView;->setAdapter(Landroidx/recyclerview/widget/RecyclerView$Adapter;)V

    .line 413
    iget-object p1, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->pickerCollapsedShadow:Landroid/view/View;

    iget-object p2, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->context:Landroid/content/Context;

    sget v1, Lcom/helpshift/R$color;->hs__color_40000000:I

    .line 414
    invoke-static {p2, v1}, Landroidx/core/content/ContextCompat;->getColor(Landroid/content/Context;I)I

    move-result p2

    const/4 v1, 0x0

    sget-object v2, Landroid/graphics/drawable/GradientDrawable$Orientation;->BOTTOM_TOP:Landroid/graphics/drawable/GradientDrawable$Orientation;

    .line 413
    invoke-static {p1, p2, v1, v2}, Lcom/helpshift/util/Styles;->setGradientBackground(Landroid/view/View;IILandroid/graphics/drawable/GradientDrawable$Orientation;)V

    .line 417
    invoke-virtual {p0}, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->hideSendReplyUI()V

    .line 418
    invoke-direct {p0, p3, p4}, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->handleSkipButtonRenderingForPicker(ZLjava/lang/String;)V

    .line 421
    invoke-virtual {p0}, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->hideKeyboard()V

    .line 424
    invoke-direct {p0, v0}, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->calculatePickerBottomOffset(Z)I

    move-result p1

    .line 425
    iget-object p2, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->parentView:Landroid/view/View;

    rsub-int p1, p1, 0x8e

    invoke-direct {p0, p2, p1}, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->setBottomOffset(Landroid/view/View;I)V

    .line 427
    invoke-direct {p0}, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->registerListeners()V

    .line 428
    invoke-direct {p0}, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->initBottomSheetCallback()V

    .line 431
    invoke-direct {p0}, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->showPickerContent()V

    .line 432
    iget-object p1, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->pickerBottomSheet:Lcom/helpshift/views/bottomsheet/HSBottomSheet;

    invoke-virtual {p1}, Lcom/helpshift/views/bottomsheet/HSBottomSheet;->show()V

    return-void
.end method

.method public showNetworkErrorFooter(I)V
    .locals 7

    .line 273
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->networkErrorFooter:Landroid/widget/LinearLayout;

    const/4 v1, 0x0

    invoke-virtual {v0, v1}, Landroid/widget/LinearLayout;->setVisibility(I)V

    .line 274
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->networkErrorFooter:Landroid/widget/LinearLayout;

    sget v2, Lcom/helpshift/R$id;->networkErrorFooterText:I

    invoke-virtual {v0, v2}, Landroid/widget/LinearLayout;->findViewById(I)Landroid/view/View;

    move-result-object v0

    check-cast v0, Landroid/widget/TextView;

    .line 275
    iget-object v2, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->networkErrorFooter:Landroid/widget/LinearLayout;

    sget v3, Lcom/helpshift/R$id;->networkErrorProgressBar:I

    invoke-virtual {v2, v3}, Landroid/widget/LinearLayout;->findViewById(I)Landroid/view/View;

    move-result-object v2

    check-cast v2, Landroid/widget/ProgressBar;

    .line 276
    iget-object v3, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->networkErrorFooter:Landroid/widget/LinearLayout;

    sget v4, Lcom/helpshift/R$id;->networkErrorIcon:I

    invoke-virtual {v3, v4}, Landroid/widget/LinearLayout;->findViewById(I)Landroid/view/View;

    move-result-object v3

    check-cast v3, Landroid/widget/ImageView;

    .line 278
    invoke-virtual {v3, v1}, Landroid/widget/ImageView;->setVisibility(I)V

    .line 279
    iget-object v4, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->context:Landroid/content/Context;

    sget v5, Lcom/helpshift/R$drawable;->hs__network_error:I

    sget v6, Lcom/helpshift/R$attr;->hs__errorTextColor:I

    invoke-static {v4, v3, v5, v6}, Lcom/helpshift/util/Styles;->setDrawable(Landroid/content/Context;Landroid/view/View;II)V

    const/16 v4, 0x8

    .line 280
    invoke-virtual {v2, v4}, Landroid/widget/ProgressBar;->setVisibility(I)V

    .line 282
    iget-object v5, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->context:Landroid/content/Context;

    invoke-virtual {v5}, Landroid/content/Context;->getResources()Landroid/content/res/Resources;

    move-result-object v5

    packed-switch p1, :pswitch_data_0

    goto :goto_0

    .line 289
    :pswitch_0
    sget p1, Lcom/helpshift/R$string;->hs__network_reconnecting_error:I

    invoke-virtual {v5, p1}, Landroid/content/res/Resources;->getString(I)Ljava/lang/String;

    move-result-object p1

    invoke-virtual {v0, p1}, Landroid/widget/TextView;->setText(Ljava/lang/CharSequence;)V

    .line 290
    invoke-virtual {v3, v4}, Landroid/widget/ImageView;->setVisibility(I)V

    .line 291
    invoke-virtual {v2, v1}, Landroid/widget/ProgressBar;->setVisibility(I)V

    goto :goto_0

    .line 285
    :pswitch_1
    sget p1, Lcom/helpshift/R$string;->hs__no_internet_error:I

    invoke-virtual {v5, p1}, Landroid/content/res/Resources;->getString(I)Ljava/lang/String;

    move-result-object p1

    invoke-virtual {v0, p1}, Landroid/widget/TextView;->setText(Ljava/lang/CharSequence;)V

    :goto_0
    return-void

    nop

    :pswitch_data_0
    .packed-switch 0x1
        :pswitch_1
        :pswitch_0
    .end packed-switch
.end method

.method public showOptionInput(Lcom/helpshift/conversation/activeconversation/message/input/OptionInput;)V
    .locals 0

    if-nez p1, :cond_0

    .line 169
    invoke-direct {p0}, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->resetReplyFieldToNormalTextInput()V

    return-void

    .line 174
    :cond_0
    invoke-virtual {p0}, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->hideSendReplyUI()V

    .line 175
    invoke-virtual {p0}, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->hideKeyboard()V

    .line 179
    invoke-virtual {p0}, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->setMessagesViewBottomPadding()V

    return-void
.end method

.method public showPickerClearButton()V
    .locals 2

    .line 505
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->pickerClearView:Landroid/widget/ImageView;

    invoke-virtual {v0}, Landroid/widget/ImageView;->isShown()Z

    move-result v0

    if-nez v0, :cond_0

    .line 506
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->pickerClearView:Landroid/widget/ImageView;

    const/4 v1, 0x0

    invoke-virtual {v0, v1}, Landroid/widget/ImageView;->setVisibility(I)V

    :cond_0
    return-void
.end method

.method public showReplyValidationFailedError(I)V
    .locals 5

    .line 207
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->parentView:Landroid/view/View;

    .line 208
    invoke-virtual {v0}, Landroid/view/View;->getResources()Landroid/content/res/Resources;

    move-result-object v0

    invoke-virtual {v0}, Landroid/content/res/Resources;->getConfiguration()Landroid/content/res/Configuration;

    move-result-object v0

    iget v0, v0, Landroid/content/res/Configuration;->orientation:I

    const/4 v1, 0x0

    const/4 v2, 0x1

    const/4 v3, 0x2

    if-ne v0, v3, :cond_0

    const/4 v0, 0x1

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    const-string v3, ""

    .line 210
    iget-object v4, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->context:Landroid/content/Context;

    invoke-virtual {v4}, Landroid/content/Context;->getResources()Landroid/content/res/Resources;

    move-result-object v4

    packed-switch p1, :pswitch_data_0

    goto :goto_1

    :pswitch_0
    if-eqz v0, :cond_1

    .line 232
    sget p1, Lcom/helpshift/R$string;->hs__landscape_date_input_validation_error:I

    invoke-virtual {v4, p1}, Landroid/content/res/Resources;->getString(I)Ljava/lang/String;

    move-result-object v3

    goto :goto_1

    .line 235
    :cond_1
    sget p1, Lcom/helpshift/R$string;->hs__date_input_validation_error:I

    invoke-virtual {v4, p1}, Landroid/content/res/Resources;->getString(I)Ljava/lang/String;

    move-result-object v3

    goto :goto_1

    :pswitch_1
    if-eqz v0, :cond_2

    .line 223
    sget p1, Lcom/helpshift/R$string;->hs__landscape_number_input_validation_error:I

    invoke-virtual {v4, p1}, Landroid/content/res/Resources;->getString(I)Ljava/lang/String;

    move-result-object v3

    goto :goto_1

    .line 226
    :cond_2
    sget p1, Lcom/helpshift/R$string;->hs__number_input_validation_error:I

    invoke-virtual {v4, p1}, Landroid/content/res/Resources;->getString(I)Ljava/lang/String;

    move-result-object v3

    goto :goto_1

    :pswitch_2
    if-eqz v0, :cond_3

    .line 214
    sget p1, Lcom/helpshift/R$string;->hs__landscape_email_input_validation_error:I

    invoke-virtual {v4, p1}, Landroid/content/res/Resources;->getString(I)Ljava/lang/String;

    move-result-object v3

    goto :goto_1

    .line 217
    :cond_3
    sget p1, Lcom/helpshift/R$string;->hs__email_input_validation_error:I

    invoke-virtual {v4, p1}, Landroid/content/res/Resources;->getString(I)Ljava/lang/String;

    move-result-object v3

    goto :goto_1

    .line 240
    :pswitch_3
    sget p1, Lcom/helpshift/R$string;->hs__conversation_detail_error:I

    invoke-virtual {v4, p1}, Landroid/content/res/Resources;->getString(I)Ljava/lang/String;

    move-result-object v3

    :goto_1
    if-eqz v0, :cond_4

    .line 246
    new-instance p1, Landroid/app/AlertDialog$Builder;

    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->parentView:Landroid/view/View;

    invoke-virtual {v0}, Landroid/view/View;->getContext()Landroid/content/Context;

    move-result-object v0

    invoke-direct {p1, v0}, Landroid/app/AlertDialog$Builder;-><init>(Landroid/content/Context;)V

    .line 248
    sget v0, Lcom/helpshift/R$string;->hs__landscape_input_validation_dialog_title:I

    invoke-virtual {v4, v0}, Landroid/content/res/Resources;->getString(I)Ljava/lang/String;

    move-result-object v0

    invoke-virtual {p1, v0}, Landroid/app/AlertDialog$Builder;->setTitle(Ljava/lang/CharSequence;)Landroid/app/AlertDialog$Builder;

    .line 249
    invoke-virtual {p1, v2}, Landroid/app/AlertDialog$Builder;->setCancelable(Z)Landroid/app/AlertDialog$Builder;

    .line 250
    invoke-virtual {p1, v3}, Landroid/app/AlertDialog$Builder;->setMessage(Ljava/lang/CharSequence;)Landroid/app/AlertDialog$Builder;

    const v0, 0x104000a

    .line 251
    new-instance v1, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer$1;

    invoke-direct {v1, p0}, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer$1;-><init>(Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;)V

    invoke-virtual {p1, v0, v1}, Landroid/app/AlertDialog$Builder;->setPositiveButton(ILandroid/content/DialogInterface$OnClickListener;)Landroid/app/AlertDialog$Builder;

    .line 258
    invoke-virtual {p1}, Landroid/app/AlertDialog$Builder;->create()Landroid/app/AlertDialog;

    move-result-object p1

    invoke-virtual {p1}, Landroid/app/AlertDialog;->show()V

    goto :goto_2

    .line 262
    :cond_4
    iget-object p1, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->replyValidationFailedView:Landroid/widget/TextView;

    invoke-virtual {p1, v3}, Landroid/widget/TextView;->setText(Ljava/lang/CharSequence;)V

    .line 263
    iget-object p1, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->replyValidationFailedView:Landroid/widget/TextView;

    invoke-virtual {p1, v1}, Landroid/widget/TextView;->setVisibility(I)V

    :goto_2
    return-void

    :pswitch_data_0
    .packed-switch 0x1
        :pswitch_3
        :pswitch_2
        :pswitch_1
        :pswitch_0
    .end packed-switch
.end method

.method public showSkipButton()V
    .locals 3

    .line 186
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->parentView:Landroid/view/View;

    .line 187
    invoke-virtual {v0}, Landroid/view/View;->getContext()Landroid/content/Context;

    move-result-object v0

    iget-object v1, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->skipBubbleTextView:Landroid/widget/TextView;

    invoke-virtual {v1}, Landroid/widget/TextView;->getBackground()Landroid/graphics/drawable/Drawable;

    move-result-object v1

    sget v2, Lcom/helpshift/R$attr;->hs__selectableOptionColor:I

    invoke-static {v0, v1, v2}, Lcom/helpshift/util/Styles;->setColorFilter(Landroid/content/Context;Landroid/graphics/drawable/Drawable;I)V

    .line 188
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->parentView:Landroid/view/View;

    invoke-virtual {v0}, Landroid/view/View;->getContext()Landroid/content/Context;

    move-result-object v0

    iget-object v1, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->skipOutterBubble:Landroid/widget/LinearLayout;

    invoke-virtual {v1}, Landroid/widget/LinearLayout;->getBackground()Landroid/graphics/drawable/Drawable;

    move-result-object v1

    const v2, 0x1010054

    invoke-static {v0, v1, v2}, Lcom/helpshift/util/Styles;->setColorFilter(Landroid/content/Context;Landroid/graphics/drawable/Drawable;I)V

    .line 189
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->skipOutterBubble:Landroid/widget/LinearLayout;

    const/4 v1, 0x0

    invoke-virtual {v0, v1}, Landroid/widget/LinearLayout;->setVisibility(I)V

    .line 194
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->messagesRecyclerView:Landroidx/recyclerview/widget/RecyclerView;

    iget-object v1, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->lastMessageItemDecor:Landroidx/recyclerview/widget/RecyclerView$ItemDecoration;

    invoke-virtual {v0, v1}, Landroidx/recyclerview/widget/RecyclerView;->removeItemDecoration(Landroidx/recyclerview/widget/RecyclerView$ItemDecoration;)V

    .line 195
    invoke-direct {p0}, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->createRecyclerViewLastItemDecor()V

    .line 196
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->messagesRecyclerView:Landroidx/recyclerview/widget/RecyclerView;

    iget-object v1, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->lastMessageItemDecor:Landroidx/recyclerview/widget/RecyclerView$ItemDecoration;

    invoke-virtual {v0, v1}, Landroidx/recyclerview/widget/RecyclerView;->addItemDecoration(Landroidx/recyclerview/widget/RecyclerView$ItemDecoration;)V

    return-void
.end method

.method public unregisterFragmentRenderer()V
    .locals 1

    .line 1285
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->messagesAdapter:Lcom/helpshift/support/conversations/MessagesAdapter;

    if-eqz v0, :cond_0

    .line 1286
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->messagesAdapter:Lcom/helpshift/support/conversations/MessagesAdapter;

    invoke-virtual {v0}, Lcom/helpshift/support/conversations/MessagesAdapter;->unregisterAdapterClickListener()V

    :cond_0
    return-void
.end method

.method public updateConversationFooterState(Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;)V
    .locals 1

    .line 1126
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->messagesAdapter:Lcom/helpshift/support/conversations/MessagesAdapter;

    if-eqz v0, :cond_1

    .line 1127
    sget-object v0, Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;->NONE:Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;

    if-eq p1, v0, :cond_0

    .line 1128
    invoke-virtual {p0}, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->hideKeyboard()V

    .line 1130
    :cond_0
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->messagesAdapter:Lcom/helpshift/support/conversations/MessagesAdapter;

    invoke-virtual {v0, p1}, Lcom/helpshift/support/conversations/MessagesAdapter;->setConversationFooterState(Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;)V

    :cond_1
    return-void
.end method

.method public updateConversationResolutionQuestionUI(Z)V
    .locals 1

    if-eqz p1, :cond_0

    .line 1006
    invoke-virtual {p0}, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->hideKeyboard()V

    .line 1007
    iget-object p1, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->confirmationBoxView:Landroid/view/View;

    const/4 v0, 0x0

    invoke-virtual {p1, v0}, Landroid/view/View;->setVisibility(I)V

    goto :goto_0

    .line 1010
    :cond_0
    iget-object p1, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->confirmationBoxView:Landroid/view/View;

    const/16 v0, 0x8

    invoke-virtual {p1, v0}, Landroid/view/View;->setVisibility(I)V

    :goto_0
    return-void
.end method

.method public updateHistoryLoadingState(Lcom/helpshift/conversation/activeconversation/message/HistoryLoadingState;)V
    .locals 1

    .line 1135
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->messagesAdapter:Lcom/helpshift/support/conversations/MessagesAdapter;

    if-eqz v0, :cond_0

    .line 1137
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->messagesAdapter:Lcom/helpshift/support/conversations/MessagesAdapter;

    invoke-virtual {v0, p1}, Lcom/helpshift/support/conversations/MessagesAdapter;->setHistoryLoadingState(Lcom/helpshift/conversation/activeconversation/message/HistoryLoadingState;)V

    :cond_0
    return-void
.end method

.method public updateImageAttachmentButtonView(Z)V
    .locals 1

    if-eqz p1, :cond_0

    .line 960
    sget-object p1, Lcom/helpshift/support/fragments/HSMenuItemType;->SCREENSHOT_ATTACHMENT:Lcom/helpshift/support/fragments/HSMenuItemType;

    const/4 v0, 0x1

    invoke-direct {p0, p1, v0}, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->changeMenuItemVisibility(Lcom/helpshift/support/fragments/HSMenuItemType;Z)V

    goto :goto_0

    .line 963
    :cond_0
    sget-object p1, Lcom/helpshift/support/fragments/HSMenuItemType;->SCREENSHOT_ATTACHMENT:Lcom/helpshift/support/fragments/HSMenuItemType;

    const/4 v0, 0x0

    invoke-direct {p0, p1, v0}, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->changeMenuItemVisibility(Lcom/helpshift/support/fragments/HSMenuItemType;Z)V

    :goto_0
    return-void
.end method

.method public updateListPickerOptions(Ljava/util/List;)V
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/viewmodel/OptionUIModel;",
            ">;)V"
        }
    .end annotation

    .line 305
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->pickerAdapter:Lcom/helpshift/support/conversations/picker/PickerAdapter;

    if-eqz v0, :cond_0

    .line 306
    invoke-direct {p0}, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->showPickerContent()V

    .line 307
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->pickerAdapter:Lcom/helpshift/support/conversations/picker/PickerAdapter;

    invoke-virtual {v0, p1}, Lcom/helpshift/support/conversations/picker/PickerAdapter;->dispatchUpdates(Ljava/util/List;)V

    :cond_0
    return-void
.end method

.method public updateMessages(II)V
    .locals 1

    .line 935
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->messagesAdapter:Lcom/helpshift/support/conversations/MessagesAdapter;

    if-nez v0, :cond_0

    return-void

    :cond_0
    if-nez p1, :cond_1

    .line 939
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->messagesAdapter:Lcom/helpshift/support/conversations/MessagesAdapter;

    invoke-virtual {v0}, Lcom/helpshift/support/conversations/MessagesAdapter;->getMessageCount()I

    move-result v0

    if-ne p2, v0, :cond_1

    .line 940
    iget-object p1, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->messagesAdapter:Lcom/helpshift/support/conversations/MessagesAdapter;

    invoke-virtual {p1}, Lcom/helpshift/support/conversations/MessagesAdapter;->notifyDataSetChanged()V

    goto :goto_0

    .line 943
    :cond_1
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->messagesAdapter:Lcom/helpshift/support/conversations/MessagesAdapter;

    invoke-virtual {v0, p1, p2}, Lcom/helpshift/support/conversations/MessagesAdapter;->onItemRangeChanged(II)V

    :goto_0
    return-void
.end method

.method public updateScrollJumperView(ZZ)V
    .locals 0

    if-eqz p1, :cond_0

    .line 1233
    invoke-direct {p0, p2}, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->showScrollJumperView(Z)V

    goto :goto_0

    .line 1236
    :cond_0
    invoke-direct {p0}, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->hideScrollJumperView()V

    :goto_0
    return-void
.end method

.method public updateSendReplyButton(Z)V
    .locals 0

    if-eqz p1, :cond_0

    .line 970
    invoke-virtual {p0}, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->enableSendReplyButton()V

    goto :goto_0

    .line 973
    :cond_0
    invoke-virtual {p0}, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->disableSendReplyButton()V

    :goto_0
    return-void
.end method

.method public updateSendReplyUI(ZLcom/helpshift/conversation/activeconversation/message/input/Input;)V
    .locals 0

    if-eqz p1, :cond_0

    .line 1017
    invoke-direct {p0, p2}, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->showSendReplyUI(Lcom/helpshift/conversation/activeconversation/message/input/Input;)V

    goto :goto_0

    .line 1020
    :cond_0
    invoke-virtual {p0}, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->hideSendReplyUI()V

    :goto_0
    return-void
.end method
