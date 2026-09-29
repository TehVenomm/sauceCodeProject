.class public Lcom/helpshift/util/HSLinkify;
.super Ljava/lang/Object;
.source "HSLinkify.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/helpshift/util/HSLinkify$LinkClickListener;,
        Lcom/helpshift/util/HSLinkify$TransformFilter;,
        Lcom/helpshift/util/HSLinkify$MatchFilter;
    }
.end annotation


# static fields
.field public static final ALL:I = 0xf

.field private static final EMAIL_ADDRESSES:I = 0x2

.field private static final MAP_ADDRESSES:I = 0x8

.field private static final PHONE_NUMBERS:I = 0x4

.field public static final WEB_URLS:I = 0x1

.field private static final sUrlMatchFilter:Lcom/helpshift/util/HSLinkify$MatchFilter;


# direct methods
.method static constructor <clinit>()V
    .locals 1

    .line 97
    new-instance v0, Lcom/helpshift/util/HSLinkify$1;

    invoke-direct {v0}, Lcom/helpshift/util/HSLinkify$1;-><init>()V

    sput-object v0, Lcom/helpshift/util/HSLinkify;->sUrlMatchFilter:Lcom/helpshift/util/HSLinkify$MatchFilter;

    return-void
.end method

.method public constructor <init>()V
    .locals 0

    .line 62
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method public static addLinks(Landroid/widget/TextView;Ljava/util/regex/Pattern;Ljava/lang/String;Lcom/helpshift/util/HSLinkify$MatchFilter;Lcom/helpshift/util/HSLinkify$TransformFilter;Lcom/helpshift/util/HSLinkify$LinkClickListener;)V
    .locals 7

    .line 440
    invoke-virtual {p0}, Landroid/widget/TextView;->getText()Ljava/lang/CharSequence;

    move-result-object v0

    invoke-static {v0}, Landroid/text/SpannableString;->valueOf(Ljava/lang/CharSequence;)Landroid/text/SpannableString;

    move-result-object v0

    move-object v1, v0

    move-object v2, p1

    move-object v3, p2

    move-object v4, p3

    move-object v5, p4

    move-object v6, p5

    .line 442
    invoke-static/range {v1 .. v6}, Lcom/helpshift/util/HSLinkify;->addLinks(Landroid/text/Spannable;Ljava/util/regex/Pattern;Ljava/lang/String;Lcom/helpshift/util/HSLinkify$MatchFilter;Lcom/helpshift/util/HSLinkify$TransformFilter;Lcom/helpshift/util/HSLinkify$LinkClickListener;)Z

    move-result p1

    if-eqz p1, :cond_1

    .line 443
    invoke-virtual {p0, v0}, Landroid/widget/TextView;->setText(Ljava/lang/CharSequence;)V

    .line 445
    invoke-virtual {p0}, Landroid/widget/TextView;->getMovementMethod()Landroid/text/method/MovementMethod;

    move-result-object p1

    if-eqz p1, :cond_0

    .line 447
    instance-of p1, p1, Landroid/text/method/LinkMovementMethod;

    if-nez p1, :cond_1

    .line 448
    :cond_0
    invoke-virtual {p0}, Landroid/widget/TextView;->getLinksClickable()Z

    move-result p1

    if-eqz p1, :cond_1

    .line 449
    invoke-static {}, Landroid/text/method/LinkMovementMethod;->getInstance()Landroid/text/method/MovementMethod;

    move-result-object p1

    invoke-virtual {p0, p1}, Landroid/widget/TextView;->setMovementMethod(Landroid/text/method/MovementMethod;)V

    :cond_1
    return-void
.end method

.method private static addLinks(Landroid/text/Spannable;ILcom/helpshift/util/HSLinkify$LinkClickListener;)Z
    .locals 19

    move-object/from16 v0, p0

    const/4 v2, 0x0

    if-nez p1, :cond_0

    return v2

    .line 123
    :cond_0
    invoke-interface/range {p0 .. p0}, Landroid/text/Spannable;->length()I

    move-result v3

    const-class v4, Landroid/text/style/URLSpan;

    invoke-interface {v0, v2, v3, v4}, Landroid/text/Spannable;->getSpans(IILjava/lang/Class;)[Ljava/lang/Object;

    move-result-object v3

    check-cast v3, [Landroid/text/style/URLSpan;

    .line 125
    array-length v4, v3

    const/4 v5, 0x1

    sub-int/2addr v4, v5

    :goto_0
    if-ltz v4, :cond_1

    .line 126
    aget-object v6, v3, v4

    invoke-interface {v0, v6}, Landroid/text/Spannable;->removeSpan(Ljava/lang/Object;)V

    add-int/lit8 v4, v4, -0x1

    goto :goto_0

    .line 129
    :cond_1
    new-instance v3, Ljava/util/ArrayList;

    invoke-direct {v3}, Ljava/util/ArrayList;-><init>()V

    and-int/lit8 v4, p1, 0x1

    if-eqz v4, :cond_8

    .line 135
    sget-object v4, Landroid/util/Patterns;->WEB_URL:Ljava/util/regex/Pattern;

    invoke-virtual {v4, v0}, Ljava/util/regex/Pattern;->matcher(Ljava/lang/CharSequence;)Ljava/util/regex/Matcher;

    move-result-object v4

    .line 137
    :cond_2
    :goto_1
    invoke-virtual {v4}, Ljava/util/regex/Matcher;->find()Z

    move-result v6

    if-eqz v6, :cond_8

    .line 138
    invoke-virtual {v4}, Ljava/util/regex/Matcher;->start()I

    move-result v6

    .line 139
    invoke-virtual {v4}, Ljava/util/regex/Matcher;->end()I

    move-result v7

    .line 141
    sget-object v8, Lcom/helpshift/util/HSLinkify;->sUrlMatchFilter:Lcom/helpshift/util/HSLinkify$MatchFilter;

    if-eqz v8, :cond_3

    sget-object v8, Lcom/helpshift/util/HSLinkify;->sUrlMatchFilter:Lcom/helpshift/util/HSLinkify$MatchFilter;

    invoke-interface {v8, v0, v6, v7}, Lcom/helpshift/util/HSLinkify$MatchFilter;->acceptMatch(Ljava/lang/CharSequence;II)Z

    move-result v8

    if-eqz v8, :cond_2

    .line 142
    :cond_3
    new-instance v8, Lcom/helpshift/util/LinkSpec;

    invoke-direct {v8}, Lcom/helpshift/util/LinkSpec;-><init>()V

    .line 144
    invoke-virtual {v4, v2}, Ljava/util/regex/Matcher;->group(I)Ljava/lang/String;

    move-result-object v15

    const-string v9, "http://"

    const-string v10, "https://"

    const-string v11, "rtsp://"

    .line 145
    filled-new-array {v9, v10, v11}, [Ljava/lang/String;

    move-result-object v14

    const/4 v13, 0x0

    .line 149
    :goto_2
    array-length v9, v14

    if-ge v13, v9, :cond_6

    const/4 v10, 0x1

    const/4 v11, 0x0

    .line 150
    aget-object v12, v14, v13

    const/16 v16, 0x0

    aget-object v9, v14, v13

    .line 151
    invoke-virtual {v9}, Ljava/lang/String;->length()I

    move-result v17

    move-object v9, v15

    move/from16 v18, v13

    move/from16 v13, v16

    move-object/from16 v16, v14

    move/from16 v14, v17

    .line 150
    invoke-virtual/range {v9 .. v14}, Ljava/lang/String;->regionMatches(ZILjava/lang/String;II)Z

    move-result v9

    if-eqz v9, :cond_5

    const/4 v10, 0x0

    const/4 v11, 0x0

    .line 155
    aget-object v12, v16, v18

    const/4 v13, 0x0

    aget-object v9, v16, v18

    .line 156
    invoke-virtual {v9}, Ljava/lang/String;->length()I

    move-result v14

    move-object v9, v15

    .line 155
    invoke-virtual/range {v9 .. v14}, Ljava/lang/String;->regionMatches(ZILjava/lang/String;II)Z

    move-result v9

    if-nez v9, :cond_4

    .line 157
    new-instance v9, Ljava/lang/StringBuilder;

    invoke-direct {v9}, Ljava/lang/StringBuilder;-><init>()V

    aget-object v10, v16, v18

    invoke-virtual {v9, v10}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    aget-object v10, v16, v18

    invoke-virtual {v10}, Ljava/lang/String;->length()I

    move-result v10

    invoke-virtual {v15, v10}, Ljava/lang/String;->substring(I)Ljava/lang/String;

    move-result-object v10

    invoke-virtual {v9, v10}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v9}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v15

    :cond_4
    const/4 v9, 0x1

    goto :goto_3

    :cond_5
    add-int/lit8 v13, v18, 0x1

    move-object/from16 v14, v16

    goto :goto_2

    :cond_6
    move-object/from16 v16, v14

    const/4 v9, 0x0

    :goto_3
    if-nez v9, :cond_7

    .line 165
    new-instance v9, Ljava/lang/StringBuilder;

    invoke-direct {v9}, Ljava/lang/StringBuilder;-><init>()V

    aget-object v10, v16, v2

    invoke-virtual {v9, v10}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v9, v15}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v9}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v15

    .line 168
    :cond_7
    iput-object v15, v8, Lcom/helpshift/util/LinkSpec;->url:Ljava/lang/String;

    .line 169
    iput v6, v8, Lcom/helpshift/util/LinkSpec;->start:I

    .line 170
    iput v7, v8, Lcom/helpshift/util/LinkSpec;->end:I

    .line 172
    invoke-virtual {v3, v8}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    goto/16 :goto_1

    :cond_8
    and-int/lit8 v4, p1, 0x2

    if-eqz v4, :cond_d

    .line 181
    sget-object v4, Landroid/util/Patterns;->EMAIL_ADDRESS:Ljava/util/regex/Pattern;

    invoke-virtual {v4, v0}, Ljava/util/regex/Pattern;->matcher(Ljava/lang/CharSequence;)Ljava/util/regex/Matcher;

    move-result-object v4

    .line 183
    :goto_4
    invoke-virtual {v4}, Ljava/util/regex/Matcher;->find()Z

    move-result v6

    if-eqz v6, :cond_d

    .line 184
    invoke-virtual {v4}, Ljava/util/regex/Matcher;->start()I

    move-result v6

    .line 185
    invoke-virtual {v4}, Ljava/util/regex/Matcher;->end()I

    move-result v7

    .line 187
    new-instance v8, Lcom/helpshift/util/LinkSpec;

    invoke-direct {v8}, Lcom/helpshift/util/LinkSpec;-><init>()V

    .line 189
    invoke-virtual {v4, v2}, Ljava/util/regex/Matcher;->group(I)Ljava/lang/String;

    move-result-object v15

    const-string v9, "mailto:"

    .line 190
    filled-new-array {v9}, [Ljava/lang/String;

    move-result-object v14

    const/4 v13, 0x0

    .line 194
    :goto_5
    array-length v9, v14

    if-ge v13, v9, :cond_b

    const/4 v10, 0x1

    const/4 v11, 0x0

    .line 195
    aget-object v12, v14, v13

    const/16 v16, 0x0

    aget-object v9, v14, v13

    .line 196
    invoke-virtual {v9}, Ljava/lang/String;->length()I

    move-result v17

    move-object v9, v15

    move/from16 v18, v13

    move/from16 v13, v16

    move-object/from16 v16, v14

    move/from16 v14, v17

    .line 195
    invoke-virtual/range {v9 .. v14}, Ljava/lang/String;->regionMatches(ZILjava/lang/String;II)Z

    move-result v9

    if-eqz v9, :cond_a

    const/4 v10, 0x0

    const/4 v11, 0x0

    .line 200
    aget-object v12, v16, v18

    const/4 v13, 0x0

    aget-object v9, v16, v18

    .line 201
    invoke-virtual {v9}, Ljava/lang/String;->length()I

    move-result v14

    move-object v9, v15

    .line 200
    invoke-virtual/range {v9 .. v14}, Ljava/lang/String;->regionMatches(ZILjava/lang/String;II)Z

    move-result v9

    if-nez v9, :cond_9

    .line 202
    new-instance v9, Ljava/lang/StringBuilder;

    invoke-direct {v9}, Ljava/lang/StringBuilder;-><init>()V

    aget-object v10, v16, v18

    invoke-virtual {v9, v10}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    aget-object v10, v16, v18

    invoke-virtual {v10}, Ljava/lang/String;->length()I

    move-result v10

    invoke-virtual {v15, v10}, Ljava/lang/String;->substring(I)Ljava/lang/String;

    move-result-object v10

    invoke-virtual {v9, v10}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v9}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v15

    :cond_9
    const/4 v9, 0x1

    goto :goto_6

    :cond_a
    add-int/lit8 v13, v18, 0x1

    move-object/from16 v14, v16

    goto :goto_5

    :cond_b
    move-object/from16 v16, v14

    const/4 v9, 0x0

    :goto_6
    if-nez v9, :cond_c

    .line 210
    new-instance v9, Ljava/lang/StringBuilder;

    invoke-direct {v9}, Ljava/lang/StringBuilder;-><init>()V

    aget-object v10, v16, v2

    invoke-virtual {v9, v10}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v9, v15}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v9}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v15

    .line 213
    :cond_c
    iput-object v15, v8, Lcom/helpshift/util/LinkSpec;->url:Ljava/lang/String;

    .line 214
    iput v6, v8, Lcom/helpshift/util/LinkSpec;->start:I

    .line 215
    iput v7, v8, Lcom/helpshift/util/LinkSpec;->end:I

    .line 217
    invoke-virtual {v3, v8}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    goto/16 :goto_4

    :cond_d
    and-int/lit8 v4, p1, 0x8

    if-eqz v4, :cond_f

    .line 224
    invoke-virtual/range {p0 .. p0}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v4

    const/4 v6, 0x0

    .line 229
    :catch_0
    :goto_7
    :try_start_0
    invoke-static {v4}, Landroid/webkit/WebView;->findAddress(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v7

    if-eqz v7, :cond_f

    .line 230
    invoke-virtual {v4, v7}, Ljava/lang/String;->indexOf(Ljava/lang/String;)I

    move-result v8

    if-gez v8, :cond_e

    goto :goto_8

    .line 236
    :cond_e
    new-instance v9, Lcom/helpshift/util/LinkSpec;

    invoke-direct {v9}, Lcom/helpshift/util/LinkSpec;-><init>()V

    .line 237
    invoke-virtual {v7}, Ljava/lang/String;->length()I

    move-result v10

    add-int/2addr v10, v8

    add-int/2addr v8, v6

    .line 240
    iput v8, v9, Lcom/helpshift/util/LinkSpec;->start:I

    add-int/2addr v6, v10

    .line 241
    iput v6, v9, Lcom/helpshift/util/LinkSpec;->end:I

    .line 242
    invoke-virtual {v4, v10}, Ljava/lang/String;->substring(I)Ljava/lang/String;

    move-result-object v4
    :try_end_0
    .catch Ljava/lang/UnsupportedOperationException; {:try_start_0 .. :try_end_0} :catch_1

    :try_start_1
    const-string v8, "UTF-8"

    .line 248
    invoke-static {v7, v8}, Ljava/net/URLEncoder;->encode(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v7
    :try_end_1
    .catch Ljava/io/UnsupportedEncodingException; {:try_start_1 .. :try_end_1} :catch_0
    .catch Ljava/lang/UnsupportedOperationException; {:try_start_1 .. :try_end_1} :catch_1

    .line 254
    :try_start_2
    new-instance v8, Ljava/lang/StringBuilder;

    invoke-direct {v8}, Ljava/lang/StringBuilder;-><init>()V

    const-string v10, "geo:0,0?q="

    invoke-virtual {v8, v10}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v8, v7}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v8}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v7

    iput-object v7, v9, Lcom/helpshift/util/LinkSpec;->url:Ljava/lang/String;

    .line 255
    invoke-virtual {v3, v9}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z
    :try_end_2
    .catch Ljava/lang/UnsupportedOperationException; {:try_start_2 .. :try_end_2} :catch_1

    goto :goto_7

    :catch_1
    :cond_f
    :goto_8
    and-int/lit8 v1, p1, 0x4

    if-eqz v1, :cond_11

    .line 269
    sget-object v1, Landroid/util/Patterns;->PHONE:Ljava/util/regex/Pattern;

    .line 270
    invoke-virtual {v1, v0}, Ljava/util/regex/Pattern;->matcher(Ljava/lang/CharSequence;)Ljava/util/regex/Matcher;

    move-result-object v1

    .line 273
    :cond_10
    :goto_9
    invoke-virtual {v1}, Ljava/util/regex/Matcher;->find()Z

    move-result v4

    if-eqz v4, :cond_11

    .line 276
    invoke-virtual {v1}, Ljava/util/regex/Matcher;->group()Ljava/lang/String;

    move-result-object v4

    .line 279
    invoke-virtual {v4}, Ljava/lang/String;->length()I

    move-result v6

    const/4 v7, 0x6

    if-lt v6, v7, :cond_10

    .line 280
    new-instance v6, Lcom/helpshift/util/LinkSpec;

    invoke-direct {v6}, Lcom/helpshift/util/LinkSpec;-><init>()V

    .line 281
    new-instance v7, Ljava/lang/StringBuilder;

    invoke-direct {v7}, Ljava/lang/StringBuilder;-><init>()V

    const-string v8, "tel:"

    invoke-virtual {v7, v8}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v7, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v7}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v4

    iput-object v4, v6, Lcom/helpshift/util/LinkSpec;->url:Ljava/lang/String;

    .line 282
    invoke-virtual {v1}, Ljava/util/regex/Matcher;->start()I

    move-result v4

    iput v4, v6, Lcom/helpshift/util/LinkSpec;->start:I

    .line 283
    invoke-virtual {v1}, Ljava/util/regex/Matcher;->end()I

    move-result v4

    iput v4, v6, Lcom/helpshift/util/LinkSpec;->end:I

    .line 284
    invoke-virtual {v3, v6}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    goto :goto_9

    .line 291
    :cond_11
    new-instance v1, Lcom/helpshift/util/HSLinkify$2;

    invoke-direct {v1}, Lcom/helpshift/util/HSLinkify$2;-><init>()V

    .line 317
    invoke-static {v3, v1}, Ljava/util/Collections;->sort(Ljava/util/List;Ljava/util/Comparator;)V

    .line 319
    invoke-virtual {v3}, Ljava/util/ArrayList;->size()I

    move-result v1

    move v4, v1

    const/4 v1, 0x0

    :goto_a
    add-int/lit8 v6, v4, -0x1

    if-ge v1, v6, :cond_16

    .line 323
    invoke-virtual {v3, v1}, Ljava/util/ArrayList;->get(I)Ljava/lang/Object;

    move-result-object v6

    check-cast v6, Lcom/helpshift/util/LinkSpec;

    add-int/lit8 v7, v1, 0x1

    .line 324
    invoke-virtual {v3, v7}, Ljava/util/ArrayList;->get(I)Ljava/lang/Object;

    move-result-object v8

    check-cast v8, Lcom/helpshift/util/LinkSpec;

    .line 327
    iget v9, v6, Lcom/helpshift/util/LinkSpec;->start:I

    iget v10, v8, Lcom/helpshift/util/LinkSpec;->start:I

    if-gt v9, v10, :cond_15

    iget v9, v6, Lcom/helpshift/util/LinkSpec;->end:I

    iget v10, v8, Lcom/helpshift/util/LinkSpec;->start:I

    if-le v9, v10, :cond_15

    .line 328
    iget v9, v8, Lcom/helpshift/util/LinkSpec;->end:I

    iget v10, v6, Lcom/helpshift/util/LinkSpec;->end:I

    const/4 v11, -0x1

    if-gt v9, v10, :cond_12

    :goto_b
    move v6, v7

    goto :goto_c

    .line 331
    :cond_12
    iget v9, v6, Lcom/helpshift/util/LinkSpec;->end:I

    iget v10, v6, Lcom/helpshift/util/LinkSpec;->start:I

    sub-int/2addr v9, v10

    iget v10, v8, Lcom/helpshift/util/LinkSpec;->end:I

    iget v12, v8, Lcom/helpshift/util/LinkSpec;->start:I

    sub-int/2addr v10, v12

    if-le v9, v10, :cond_13

    goto :goto_b

    .line 334
    :cond_13
    iget v9, v6, Lcom/helpshift/util/LinkSpec;->end:I

    iget v6, v6, Lcom/helpshift/util/LinkSpec;->start:I

    sub-int/2addr v9, v6

    iget v6, v8, Lcom/helpshift/util/LinkSpec;->end:I

    iget v8, v8, Lcom/helpshift/util/LinkSpec;->start:I

    sub-int/2addr v6, v8

    if-ge v9, v6, :cond_14

    move v6, v1

    goto :goto_c

    :cond_14
    const/4 v6, -0x1

    :goto_c
    if-eq v6, v11, :cond_15

    .line 339
    invoke-virtual {v3, v6}, Ljava/util/ArrayList;->remove(I)Ljava/lang/Object;

    add-int/lit8 v4, v4, -0x1

    goto :goto_a

    :cond_15
    move v1, v7

    goto :goto_a

    .line 349
    :cond_16
    invoke-virtual {v3}, Ljava/util/ArrayList;->size()I

    move-result v1

    if-nez v1, :cond_17

    return v2

    .line 353
    :cond_17
    invoke-virtual {v3}, Ljava/util/ArrayList;->iterator()Ljava/util/Iterator;

    move-result-object v1

    :goto_d
    invoke-interface {v1}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_18

    invoke-interface {v1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Lcom/helpshift/util/LinkSpec;

    .line 355
    new-instance v3, Lcom/helpshift/util/HSLinkify$3;

    iget-object v4, v2, Lcom/helpshift/util/LinkSpec;->url:Ljava/lang/String;

    move-object/from16 v6, p2

    invoke-direct {v3, v4, v6, v2}, Lcom/helpshift/util/HSLinkify$3;-><init>(Ljava/lang/String;Lcom/helpshift/util/HSLinkify$LinkClickListener;Lcom/helpshift/util/LinkSpec;)V

    .line 368
    iget v4, v2, Lcom/helpshift/util/LinkSpec;->start:I

    iget v2, v2, Lcom/helpshift/util/LinkSpec;->end:I

    const/16 v7, 0x21

    invoke-interface {v0, v3, v4, v2, v7}, Landroid/text/Spannable;->setSpan(Ljava/lang/Object;III)V

    goto :goto_d

    :cond_18
    return v5
.end method

.method private static addLinks(Landroid/text/Spannable;Ljava/util/regex/Pattern;Ljava/lang/String;Lcom/helpshift/util/HSLinkify$MatchFilter;Lcom/helpshift/util/HSLinkify$TransformFilter;Lcom/helpshift/util/HSLinkify$LinkClickListener;)Z
    .locals 19

    move-object/from16 v0, p0

    move-object/from16 v1, p2

    move-object/from16 v2, p3

    move-object/from16 v3, p4

    if-nez v1, :cond_0

    const-string v1, ""

    :goto_0
    move-object v4, v1

    move-object/from16 v1, p1

    goto :goto_1

    .line 473
    :cond_0
    sget-object v4, Ljava/util/Locale;->ROOT:Ljava/util/Locale;

    invoke-virtual {v1, v4}, Ljava/lang/String;->toLowerCase(Ljava/util/Locale;)Ljava/lang/String;

    move-result-object v1

    goto :goto_0

    .line 474
    :goto_1
    invoke-virtual {v1, v0}, Ljava/util/regex/Pattern;->matcher(Ljava/lang/CharSequence;)Ljava/util/regex/Matcher;

    move-result-object v1

    const/4 v5, 0x0

    const/4 v6, 0x0

    .line 476
    :goto_2
    invoke-virtual {v1}, Ljava/util/regex/Matcher;->find()Z

    move-result v7

    if-eqz v7, :cond_8

    .line 477
    invoke-virtual {v1}, Ljava/util/regex/Matcher;->start()I

    move-result v7

    .line 478
    invoke-virtual {v1}, Ljava/util/regex/Matcher;->end()I

    move-result v8

    const/4 v9, 0x1

    if-eqz v2, :cond_1

    .line 482
    invoke-interface {v2, v0, v7, v8}, Lcom/helpshift/util/HSLinkify$MatchFilter;->acceptMatch(Ljava/lang/CharSequence;II)Z

    move-result v10

    goto :goto_3

    :cond_1
    const/4 v10, 0x1

    :goto_3
    if-eqz v10, :cond_7

    .line 488
    invoke-virtual {v1, v5}, Ljava/util/regex/Matcher;->group(I)Ljava/lang/String;

    move-result-object v6

    .line 489
    new-array v10, v9, [Ljava/lang/String;

    aput-object v4, v10, v5

    if-eqz v3, :cond_2

    .line 491
    invoke-interface {v3, v1, v6}, Lcom/helpshift/util/HSLinkify$TransformFilter;->transformUrl(Ljava/util/regex/Matcher;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v6

    :cond_2
    const/4 v15, 0x0

    .line 496
    :goto_4
    array-length v11, v10

    if-ge v15, v11, :cond_5

    const/4 v12, 0x1

    const/4 v13, 0x0

    .line 497
    aget-object v14, v10, v15

    const/16 v16, 0x0

    aget-object v11, v10, v15

    .line 498
    invoke-virtual {v11}, Ljava/lang/String;->length()I

    move-result v17

    move-object v11, v6

    move/from16 v18, v15

    move/from16 v15, v16

    move/from16 v16, v17

    .line 497
    invoke-virtual/range {v11 .. v16}, Ljava/lang/String;->regionMatches(ZILjava/lang/String;II)Z

    move-result v11

    if-eqz v11, :cond_4

    const/4 v12, 0x0

    const/4 v13, 0x0

    .line 502
    aget-object v14, v10, v18

    const/4 v15, 0x0

    aget-object v11, v10, v18

    .line 503
    invoke-virtual {v11}, Ljava/lang/String;->length()I

    move-result v16

    move-object v11, v6

    .line 502
    invoke-virtual/range {v11 .. v16}, Ljava/lang/String;->regionMatches(ZILjava/lang/String;II)Z

    move-result v11

    if-nez v11, :cond_3

    .line 504
    new-instance v11, Ljava/lang/StringBuilder;

    invoke-direct {v11}, Ljava/lang/StringBuilder;-><init>()V

    aget-object v12, v10, v18

    invoke-virtual {v11, v12}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    aget-object v12, v10, v18

    invoke-virtual {v12}, Ljava/lang/String;->length()I

    move-result v12

    invoke-virtual {v6, v12}, Ljava/lang/String;->substring(I)Ljava/lang/String;

    move-result-object v6

    invoke-virtual {v11, v6}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v11}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v6

    :cond_3
    move-object v11, v6

    const/4 v6, 0x1

    goto :goto_5

    :cond_4
    add-int/lit8 v15, v18, 0x1

    goto :goto_4

    :cond_5
    move-object v11, v6

    const/4 v6, 0x0

    :goto_5
    if-nez v6, :cond_6

    .line 512
    new-instance v6, Ljava/lang/StringBuilder;

    invoke-direct {v6}, Ljava/lang/StringBuilder;-><init>()V

    aget-object v10, v10, v5

    invoke-virtual {v6, v10}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v6, v11}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v6}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v11

    .line 518
    :cond_6
    new-instance v6, Lcom/helpshift/util/HSLinkify$4;

    move-object/from16 v10, p5

    invoke-direct {v6, v11, v10, v11}, Lcom/helpshift/util/HSLinkify$4;-><init>(Ljava/lang/String;Lcom/helpshift/util/HSLinkify$LinkClickListener;Ljava/lang/String;)V

    const/16 v11, 0x21

    .line 531
    invoke-interface {v0, v6, v7, v8, v11}, Landroid/text/Spannable;->setSpan(Ljava/lang/Object;III)V

    const/4 v6, 0x1

    goto/16 :goto_2

    :cond_7
    move-object/from16 v10, p5

    goto/16 :goto_2

    :cond_8
    return v6
.end method

.method public static addLinks(Landroid/widget/TextView;ILcom/helpshift/util/HSLinkify$LinkClickListener;)Z
    .locals 4

    const/4 v0, 0x0

    if-nez p1, :cond_0

    return v0

    .line 385
    :cond_0
    invoke-virtual {p0}, Landroid/widget/TextView;->getText()Ljava/lang/CharSequence;

    move-result-object v1

    .line 387
    instance-of v2, v1, Landroid/text/Spannable;

    const/4 v3, 0x1

    if-eqz v2, :cond_4

    .line 388
    check-cast v1, Landroid/text/Spannable;

    invoke-static {v1, p1, p2}, Lcom/helpshift/util/HSLinkify;->addLinks(Landroid/text/Spannable;ILcom/helpshift/util/HSLinkify$LinkClickListener;)Z

    move-result p1

    if-eqz p1, :cond_3

    .line 390
    invoke-virtual {p0}, Landroid/widget/TextView;->getMovementMethod()Landroid/text/method/MovementMethod;

    move-result-object p1

    if-eqz p1, :cond_1

    .line 392
    instance-of p1, p1, Landroid/text/method/LinkMovementMethod;

    if-nez p1, :cond_2

    .line 393
    :cond_1
    invoke-virtual {p0}, Landroid/widget/TextView;->getLinksClickable()Z

    move-result p1

    if-eqz p1, :cond_2

    .line 394
    invoke-static {}, Landroid/text/method/LinkMovementMethod;->getInstance()Landroid/text/method/MovementMethod;

    move-result-object p1

    invoke-virtual {p0, p1}, Landroid/widget/TextView;->setMovementMethod(Landroid/text/method/MovementMethod;)V

    :cond_2
    return v3

    :cond_3
    return v0

    .line 403
    :cond_4
    invoke-static {v1}, Landroid/text/SpannableString;->valueOf(Ljava/lang/CharSequence;)Landroid/text/SpannableString;

    move-result-object v1

    .line 405
    invoke-static {v1, p1, p2}, Lcom/helpshift/util/HSLinkify;->addLinks(Landroid/text/Spannable;ILcom/helpshift/util/HSLinkify$LinkClickListener;)Z

    move-result p1

    if-eqz p1, :cond_7

    .line 407
    invoke-virtual {p0}, Landroid/widget/TextView;->getMovementMethod()Landroid/text/method/MovementMethod;

    move-result-object p1

    if-eqz p1, :cond_5

    .line 409
    instance-of p1, p1, Landroid/text/method/LinkMovementMethod;

    if-nez p1, :cond_6

    .line 410
    :cond_5
    invoke-virtual {p0}, Landroid/widget/TextView;->getLinksClickable()Z

    move-result p1

    if-eqz p1, :cond_6

    .line 411
    invoke-static {}, Landroid/text/method/LinkMovementMethod;->getInstance()Landroid/text/method/MovementMethod;

    move-result-object p1

    invoke-virtual {p0, p1}, Landroid/widget/TextView;->setMovementMethod(Landroid/text/method/MovementMethod;)V

    .line 414
    :cond_6
    invoke-virtual {p0, v1}, Landroid/widget/TextView;->setText(Ljava/lang/CharSequence;)V

    return v3

    :cond_7
    return v0
.end method
