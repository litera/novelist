set nocount on;

print 'Test data:';

print 'Adding generic test users'
	insert dbo.[User] (Name, Email, SimpleHash)
	values
		('Robert Koritnik', 'r@k', -198892813); -- password: koji

print 'Adding few test posts'
	insert dbo.Post (Title, Description, Author)
	values
		(
		'Sega Mega Drive: why retro consoles are about more than nostalgia',
		'<p class="intro">A new version of the classic1988 games console is selling well this year, apparently, but the appeal of this system goes deeper than reminiscence.</p><p>Every year at about this time, newspapers love to identify the biggest-selling Christmas presents – those sudden surprise hits that have desperate shoppers combing internet stores for hours or simply fighting each other in Toys R Us.	</p><p>This year, among other candidates, is a £40 wireless version of the Sega Mega Drive, the classic 1988 games console, famous for Sonic the Hedgehog. According to Argos, sales of the retro gadget, which includes 80 built-in games (although only 40 are Mega Drive classics, the rest are generic mini-games like solitaire) and a cartridge port so you can use any original game carts you have lying around, have risen by 400% this month. While we’re all supposed to be saving up for cutting-edge machines like the PlayStation 4 and Xbox One, some families will be gathered around a very different audio-visual experience come 25 December.</p>',
		1),
		(
		'Google offers legal support to some YouTube users in copyright battles',
		'<p class="intro">The tech company pledges to cover legal costs for handful of videos that it claims represent clear fair use despite being issued DMCA takedown notices</p><p>Google is stepping up its defense of YouTube users who find themselves on the wrong side of a copyright claim, the tech company said on Thursday.</p><p>After a series of skirmishes with established media and others the company said it was “offering legal support to a handful of videos that we believe represent clear fair uses which have been subject to DMCA [Digital Millennium Copyright Act] takedowns”.</p><p>Google’s move comes after privacy group Electronic Frontier Foundation successfully defended Stephanie Lenz, a mother whose 29-second video of her son dancing to Prince’s Let’s Go Crazy had been removed from YouTube after Universal Music issued a DMCA notice ordering it to be taken down.</p><p>“With approval of the video creators, we’ll keep the videos live on YouTube in the US, feature them in the YouTube Copyright Center as strong examples of fair use, and cover the cost of any copyright lawsuits brought against them,” Fred von Lohmann, Google’s copyright legal director, wrote on the Google blog.</p><p>Von Lohmann admitted that Google was not going to shoulder the court costs of every user on its site – not even every user with a solid case – but he said the company did want to demystify the process by which users could wield the law as effectively as entertainment companies. “We’re doing this because we recognize that creators can be intimidated by the DMCA’s counter notification process, and the potential for litigation that comes with it,” he wrote.</p>',
		1);

set nocount off;